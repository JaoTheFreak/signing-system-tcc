using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using Signing.System.Tcc.Application.Interfaces;
using Signing.System.Tcc.Domain.EtherAggregate;
using Signing.System.Tcc.Domain.RecordAggregate;
using Signing.System.Tcc.Ethereum.Interfaces;
using Signing.System.Tcc.MVC.ViewModels.Document;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing;
using System.Security.Claims;
using System;
using System.Net;
using System.Globalization;
using Signing.System.Tcc.MVC.Interfaces;

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
            
            if (string.IsNullOrEmpty(registeredImage.ImageHash))
            {
                return NotFound();
            }
            else
            {
                try
                {
                    var recordFromDb = _recordAppService.FirstOrDefault(r => r.MidiaHash.Equals(registeredImage.ImageHash) && r.User.DocumentNumber.Equals(registeredImage.AuthorDocument));

                    recordFromDb.User = _userAppService.FirstOrDefault(u => u.Id == recordFromDb.UserId);

                    var jsonToReturn = new
                    {
                        imagePath = recordFromDb.MidiaUrl,
                        imageHash = registeredImage.ImageHash,
                        transactionId = recordFromDb.TransactionHash,
                        authorImageName = recordFromDb.User.DisplayName,
                        authoDocumentId = Convert.ToUInt64(registeredImage.AuthorDocument).ToString(@"000\.000\.000\-00"),
                        imageRegisterDate = registeredImage.CreatedAt.ToString("dd/mm/yyyy HH:MM:ss", new CultureInfo("pt-BR")),
                        documentName = recordFromDb.MidiaName,
                        documentFormat = recordFromDb.MidiaExtension,
                        documentSize = $"{recordFromDb.MidiaResolution} px",
                        documentDescription = recordFromDb.MidiaDescription
                    };

                    return Json(jsonToReturn);
                }
                catch (Exception ex)
                {
                    return StatusCode((int) HttpStatusCode.BadRequest);
                }
            }
        }

        [HttpGet]
        public IActionResult NewDocument()
        {
            return View();
        }

        [HttpPost, ActionName("NewDocument")]
        public async Task<IActionResult> NewDocumentPost([FromServices] IEtherFactory etherFactory, [FromServices] IStorageService _storageService, DocumentRegisterViewModel newDocument)
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
                var authorDocument = User.Claims.FirstOrDefault(c => c.Type.Equals("Document"))?.Value;

                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.PrimarySid))?.Value);

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

                var transactionInfo = await _smartContract.RegisterImageAsync(authorDocument, newDocument.ImageHash, await etherFactory.CreateEtherAsync());

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

                var imageLink = await _storageService.UploadImageAsync(newDocument.Image, newDocument.ImageHash);

                using (var image = Image.FromStream(newDocument.Image.OpenReadStream()))
                {    
                    newRecord = _recordFactory.Create(transactionInfo.txHash, transactionInfo.txFee, newDocument.ImageHash, newDocument.DocumentDescription, newDocument.DocumentName, $"{image.PhysicalDimension.Width}x{image.PhysicalDimension.Height}", newDocument.Image.ContentType.Split('/')[1], newDocument.Image.Length, imageLink);
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

            var authorDocument = User.Claims.FirstOrDefault(c => c.Type.Equals("Document"))?.Value;

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

            var modelList = new List<DocumentsRegisteredViewModel> {
                new DocumentsRegisteredViewModel
                {
                    ArtName = "Foto Paisagem",
                    RegisterDate = DateTime.Now,
                    RegisterSuccessTrue = true,
                    ImageUrl = "https://pbs.twimg.com/profile_images/955211651371995137/3iIrG83t.jpg",
                    ImageHash = "0x1"


                },
                new DocumentsRegisteredViewModel
                {
                     ArtName = "Foto Rosto",
                    RegisterDate = DateTime.Now,
                    RegisterSuccessTrue = true,
                    ImageUrl = "https://pbs.twimg.com/profile_images/955211651371995137/3iIrG83t.jpg",
                    ImageHash = "0x2"

                },
                new DocumentsRegisteredViewModel
                {
                    ArtName = "Foto Ruas",
                    RegisterDate = DateTime.Now,
                    RegisterSuccessTrue = false,
                    ImageUrl = "https://pbs.twimg.com/profile_images/955211651371995137/3iIrG83t.jpg",
                    ImageHash = "0x3"
            var loggedUserId = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.PrimarySid)).Value);

            var recordsFromDb = _recordAppService.Find(r => r.UserId == loggedUserId);

            return View(modelList);
        }
    }
}