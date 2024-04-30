using Microsoft.AspNetCore.Mvc;


namespace MovieShop_Oregano.Controllers
{
    public class AdminController : Controller
    {

        
        public IActionResult Login()
        {

            return View();

        }

        [HttpPost]
        public IActionResult Login(string password)
        {
            if (password == "Oregano")
            {
                HttpContext.Session.SetString("IsAuthenticated", "true");
				return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("Password", "Incorrect password");
                return View();
            }
        }

        public IActionResult LogOut()
        {
            HttpContext.Session.Remove("IsAuthenticated");
            return RedirectToAction("Index", "Home");
        }

    }
}
