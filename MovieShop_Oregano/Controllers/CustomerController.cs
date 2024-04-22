using Microsoft.AspNetCore.Mvc;

namespace MovieShop_Oregano.Controllers
{
    public class CustomerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
