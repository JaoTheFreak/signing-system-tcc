using Microsoft.AspNetCore.Mvc;

namespace Signing.System.Tcc.MVC.Controllers
{
    public class DocumentController : Controller
    {
        [HttpGet]
        public string Index()
        {
            return "Oi";
        }

        [HttpGet("/checksignature/{documentSignature?}")]
        public IActionResult CheckSignature(string documentSignature)
        {
            if(string.IsNullOrEmpty(documentSignature))
            {
                return View();
            }

            return View();
        }
    }
}