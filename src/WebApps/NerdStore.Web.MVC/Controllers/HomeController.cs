using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NerdStore.Web.MVC.Models;
using System.Diagnostics;

namespace NerdStore.Web.MVC.Controllers
{
    public class HomeController : Controller
    {        
        public HomeController()
        {
            
        }

        public IActionResult Index()
        {
            return RedirectToAction("Index", "AdminProdutos");
            //return View();
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
