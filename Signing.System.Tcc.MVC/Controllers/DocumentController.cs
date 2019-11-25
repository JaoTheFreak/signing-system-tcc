using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Signing.System.Tcc.Domain.EtherAggregate;
using Signing.System.Tcc.Ethereum.Interfaces;
using Signing.System.Tcc.MVC.ViewModels.Document;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.IO;
using System.Globalization;
using Signing.System.Tcc.Application.Interfaces;

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
                documentDescription = "Fotografia seu qualquer"

            };

            return Json(jsonToReturn);
        }
        
        [HttpGet]
        public async Task<IActionResult> NewDocument([FromServices] ISmartContract smartContract)
        {
            //var doc = "06914456992";

            //var hash = "9129043929f1309a95ad8b5039185c92b2fff3913d5ed3872a414e266cb94686";

            //await smartContract.VerifyImageByAuthorDocumentAsync(doc);

            ////await smartContract.RegisterImageAsync(doc, hash);            

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

        [HttpPost, ActionName("NewDocument")]
        public IActionResult NewDocumentPost(DocumentRegisterViewModel newDocument)
        {
            return View();
        }

        [HttpGet]
        public IActionResult Index([FromServices] IEtherFactory etherFactory)
        {


            return View();
        }
    }
}