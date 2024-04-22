using Microsoft.AspNetCore.Mvc;

namespace MovieShop_Oregano.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
