using Microsoft.AspNetCore.Mvc;
using MovieShop_Oregano.Models;
using Newtonsoft.Json;

namespace MovieShop_Oregano.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        List<Movie> MovieL = new List<Movie>()
        {
            new Movie(1,"Title", "Director", 2001,100,"https://via.placeholder.com/100"),
            new Movie(2,"Title", "Director", 2001,100,"https://via.placeholder.com/100"),
            new Movie(3,"Title", "Director", 2001,100,"https://via.placeholder.com/100"),
            new Movie(4,"Title", "Director", 2001,100,"https://via.placeholder.com/100"),
            new Movie(5,"Title", "Director", 2001,100,"https://via.placeholder.com/100"),
            new Movie(6,"Title", "Director", 2001,100,"https://via.placeholder.com/100")
        };
        public IActionResult OrderConfirmation()
        {
            if (HttpContext.Session.GetString("sList") != null)
            {
                List<Movie> StudentList = JsonConvert.DeserializeObject<List<Movie>>(HttpContext.Session.GetString("sList"));
                return View(StudentList);
            }
            else
            {
                return View(MovieL);
            }
        }
    }
}
