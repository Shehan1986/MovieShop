using Microsoft.AspNetCore.Mvc;

namespace MovieShop_Oregano.Controllers
{
    public class MovieController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
