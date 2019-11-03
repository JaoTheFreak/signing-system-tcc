using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Signing.System.Tcc.MVC.Controllers
{
    public class DocumentController : Controller
    {
        [HttpGet]
        public IActionResult Validate([FromQuery] string imageHashToValidate)
        {
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


    }
}