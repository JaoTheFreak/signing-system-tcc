using Microsoft.AspNetCore.Mvc;
using Signing.System.Tcc.MVC.Models;
using System.Diagnostics;

namespace Signing.System.Tcc.MVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult DashBoard()
        {
            return View();
        }
    }
}
