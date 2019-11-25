using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using Signing.System.Tcc.Application.Interfaces;
using Signing.System.Tcc.Domain.EtherAggregate;
using Signing.System.Tcc.Domain.RecordAggregate;
using Signing.System.Tcc.Ethereum.Interfaces;
using Signing.System.Tcc.MVC.ViewModels.Document;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing;
using System.Security.Claims;
using System;

namespace Signing.System.Tcc.MVC.Controllers
{
    public class DocumentController : Controller
    {
        private readonly ISmartContract _smartContract;

        private readonly IRecordAppService _recordAppService;

        private readonly IRecordFactory _recordFactory;

        private readonly IToastNotification _notificationService;

        private readonly IUserAppService _userAppService;

        private readonly IUnitOfWorkAppService _unitOfWorkAppService;

        public DocumentController(ISmartContract smartContract, IRecordAppService recordAppService, IRecordFactory recordFactory, IUserAppService userAppService, IUnitOfWorkAppService unitOfWorkAppService, IToastNotification notificationService)
        {
            _smartContract = smartContract;

            _recordAppService = recordAppService;

            _recordFactory = recordFactory;

            _notificationService = notificationService;

            _userAppService = userAppService;

            _unitOfWorkAppService = unitOfWorkAppService;
        }

        [HttpGet]
        public async Task<IActionResult> Validate([FromQuery] string imageHashToValidate)
        {
            var registeredImage = await _smartContract.VerifyImageByHashAsync(imageHashToValidate);

            var errorCode = 0;

            var errorMessage = string.Empty;

            if (string.IsNullOrEmpty(registeredImage.ImageHash))
            {
                errorCode = 404;

                errorMessage = "Rercord Not Found";
            }
            else
            {

            }

            var jsonToReturn = new
            {
                imagePath = "https://pbs.twimg.com/profile_images/955211651371995137/3iIrG83t.jpg",
                imageHash = $"0x{registeredImage.ImageHash}",
                transactionId = "66x80c43e8380daa8213cac07f98f7909ee0b224ddbc7d1571591dbc42e3a57f",
                authorImageName = "Maycon",
                authoDocumentId = string.Format("###.###.###-##", registeredImage.AuthorDocument),
                imageRegisterDate = registeredImage.CreatedAt.ToString("dd/mm/yyyy HH:MM:ss"),
                documentName = "Nome do documento",
                documentFormat = ".jpg",
                documentSize = "500x500 px",
                documentDescription = "Fotografia seu qualquer",
                error = new
                {
                    code = errorCode,
                    message = errorMessage
                }
            };

            return Json(jsonToReturn);
        }

        [HttpGet]
        public IActionResult NewDocument()
        {
            return View();
        }

        [HttpPost, ActionName("NewDocument")]
        public async Task<IActionResult> NewDocumentPost(DocumentRegisterViewModel newDocument)
        {
            if (!ModelState.IsValid)
            {
                _notificationService.AddErrorToastMessage($"Campos obrigatórios verificar!",
                new ToastrOptions
                {
                    Title = "Erro ao enviar formulário!",
                    CloseButton = true,
                    TimeOut = 30000,
                    ProgressBar = true
                });

                return View();
            }

            var registerFound = await _smartContract.VerifyImageByHashAsync(newDocument.ImageHash);

            if (!string.IsNullOrEmpty(registerFound.ImageHash))
            {

                _notificationService.AddErrorToastMessage($"O Documento {registerFound.ImageHash} já registrado para o Autor {registerFound.AuthorDocument} na data {registerFound.CreatedAt:dd/mm/yyyy HH:MM:ss}!",
                new ToastrOptions
                {
                    Title = "Documento Já Registrado!",
                    CloseButton = true,
                    TimeOut = 300000,
                    ProgressBar = true
                });

                return View();
            }
            else
            {
                var authorDocument = User.Claims.Where(c => c.Type.Equals("Document")).FirstOrDefault()?.Value;

                var userId = Convert.ToInt32(User.Claims.Where(c => c.Type.Equals(ClaimTypes.PrimarySid)).FirstOrDefault()?.Value);

                var userFromDatabase = _userAppService.FirstOrDefault(u => u.Id == userId);

                if(userFromDatabase == null)
                {
                    _notificationService.AddErrorToastMessage($"Erro ao identificar usuário!",
                    new ToastrOptions
                    {
                        Title = "Erro!",
                        CloseButton = true,
                        TimeOut = 300000,
                        ProgressBar = true
                    });

                    return View();
                }

                var transactionInfo = await _smartContract.RegisterImageAsync(authorDocument, newDocument.ImageHash);

                if (transactionInfo.txFee == -1)
                {
                    _notificationService.AddErrorToastMessage($"Ocorreu um erro ao tentar registrar!",
                    new ToastrOptions
                    {
                        Title = "Erro durante Registro",
                        CloseButton = true,
                        TimeOut = 30000,
                        ProgressBar = true
                    });

                    return View();
                }

                Record newRecord;

                //TODO: VERIFICAR UPLOAD PARA GOOGLE CLOUD OU OUTRO LUGAR

                using (var image = Image.FromStream(newDocument.Image.OpenReadStream()))
                {    
                    newRecord = _recordFactory.Create(transactionInfo.txHash, transactionInfo.txFee, $"0x{newDocument.ImageHash}", newDocument.DocumentDescription, newDocument.DocumentName, $"{image.PhysicalDimension.Width}x{image.PhysicalDimension.Height}", newDocument.Image.ContentType.Split('/')[1], newDocument.Image.Length, "");
                }

                newRecord.User = userFromDatabase;

                _recordAppService.Add(newRecord);

                _unitOfWorkAppService.Complete();

                _notificationService.AddSuccessToastMessage($"Parabéns {userFromDatabase.DisplayName}, você acaba de registrar um novo documento!",
                    new ToastrOptions
                    {
                        Title = "Registro Efetuado com Sucesso!",
                        CloseButton = true,
                        TimeOut = 30000,
                        ProgressBar = true
                    });
            }

            return View();
        }

        [HttpPost, ActionName("Quote")]
        public async Task<IActionResult> Quote([FromServices] IEtherFactory etherFactory, string imageHashToQuote)
        {
            var registerFound = await _smartContract.VerifyImageByHashAsync(imageHashToQuote);

            if (!string.IsNullOrEmpty(registerFound.ImageHash))
            {

                _notificationService.AddErrorToastMessage($"O Documento {registerFound.ImageHash} já registrado para o Autor {registerFound.AuthorDocument} na data {registerFound.CreatedAt:dd/mm/yyyy HH:MM:ss}!",
                new ToastrOptions
                {
                    Title = "Documento Já Registrado!",
                    CloseButton = true,
                    TimeOut = 300000,
                    ProgressBar = true
                });

                return BadRequest();
            }

            var authorDocument = User.Claims.Where(c => c.Type.Equals("Document")).FirstOrDefault()?.Value;

            var billPrice = await _smartContract.EstimateTransactionPriceAsync(authorDocument, imageHashToQuote, await etherFactory.CreateEtherAsync());

            var jsonToReturn = new
            {
                gasPrice = billPrice.ToString()
            };

            return Json(jsonToReturn);
        }

        [HttpGet]
        public IActionResult Index([FromServices] IEtherFactory etherFactory)
        {


            return View();
        }
    }
}