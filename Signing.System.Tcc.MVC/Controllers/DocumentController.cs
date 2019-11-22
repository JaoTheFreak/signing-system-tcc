using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Signing.System.Tcc.Domain.EtherAggregate;
using Signing.System.Tcc.Ethereum.Interfaces;
using System;
using System.Threading.Tasks;

namespace Signing.System.Tcc.MVC.Controllers
{
    public class DocumentController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Validate([FromServices] ISmartContract  smartContract, [FromQuery] string imageHashToValidate)
        {
            var registeredImage = await smartContract.VerifyImageByHashAsync(imageHashToValidate);

            var jsonToReturn = new 
            { 
                imagePath = "https://pbs.twimg.com/profile_images/955211651371995137/3iIrG83t.jpg",
                imageHash = "5bc80c43e8380daa8538cac07f98f7909ee0b224ddbc7d1571591dbc42e3a57f",
                transactionId = "66x80c43e8380daa8213cac07f98f7909ee0b224ddbc7d1571591dbc42e3a57f",
                authorImageName = "Maycon",
                authoDocumentId = "024-024-024",
                imageRegisterDate = DateTime.Now.ToString(),
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
            var doc = "06914456992";

            var hash = "9129043929f1309a95ad8b5039185c92b2fff3913d5ed3872a414e266cb94686";

            await smartContract.VerifyImageByAuthorDocumentAsync(doc);

            //await smartContract.RegisterImageAsync(doc, hash);            

            return View();
        }

        [HttpPost, ActionName("Quote")]
        public IActionResult Quote(IFormFile file)
        {
            var jsonToReturn = new
            {
                gasPrice = "1,00"
            };

            return Json(jsonToReturn);
        }

        [HttpPost, ActionName("NewDocument")]
        public IActionResult NewDocumentPost()
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