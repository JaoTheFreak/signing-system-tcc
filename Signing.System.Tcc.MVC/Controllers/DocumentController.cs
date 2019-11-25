using Microsoft.AspNetCore.Mvc;
using Signing.System.Tcc.Application.Interfaces;
using Signing.System.Tcc.Domain.EtherAggregate;
using Signing.System.Tcc.Ethereum.Interfaces;
using Signing.System.Tcc.MVC.ViewModels.Document;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Signing.System.Tcc.MVC.Controllers
{
    public class DocumentController : Controller
    {
        private readonly ISmartContract _smartContract;

        private readonly IUserAppService _userAppService;

        public DocumentController(ISmartContract smartContract, IUserAppService userAppService)
        {
            _smartContract = smartContract;
            _userAppService = userAppService;
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
            if (!ModelState.IsValid) // ERRO
                return View();

            var registerFound = await _smartContract.VerifyImageByHashAsync("");

            if (!string.IsNullOrEmpty(registerFound.ImageHash)) //ERRO
                return View();

            return View();
        }

        [HttpPost, ActionName("Quote")]
        public async Task<IActionResult> Quote([FromServices] IEtherFactory etherFactory, string imageHashToQuote)
        {
            var authorDocument = User.Claims.Where(c => c.Type.Equals("Document")).FirstOrDefault()?.Value;
            
            var hashImage = imageHashToQuote;

            var billPrice = await _smartContract.EstimateTransactionPriceAsync(authorDocument, hashImage, await etherFactory.CreateEtherAsync());
                       
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

                },
            };

            return View(modelList);
        }
    }
}