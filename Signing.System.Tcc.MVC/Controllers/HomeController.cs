using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Signin.System.Tcc.Ethereum.Wallet;
using Signing.System.Tcc.MVC.Models;

namespace Signing.System.Tcc.MVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var q = new WalletService();

            var qq = q.GetWallet();
            
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
    }
}
