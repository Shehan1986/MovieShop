using Microsoft.AspNetCore.Mvc;
using MovieShop_Oregano.Models;
using System.Diagnostics;

namespace MovieShop_Oregano.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {

			/*var isAuthenticatedString = HttpContext.Session.GetString("IsAuthenticated");
			var isAuthenticated = !string.IsNullOrEmpty(isAuthenticatedString) && isAuthenticatedString == "true";

			return View(isAuthenticated);*/
            return View();
        }

       /*
        public IActionResult Admin()
        {

			return View();

        }

        [HttpPost]
        public IActionResult Admin(string password)
        {
            if (password == "Oregano")
            {
                HttpContext.Session.SetString("IsAuthenticated", "true");
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("Password", "Incorrect password");
                return View();
            }
        }

        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");            
        }*/

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
