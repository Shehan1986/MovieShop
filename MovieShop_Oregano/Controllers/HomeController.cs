using Microsoft.AspNetCore.Mvc;
using MovieShop_Oregano.Models;
using MovieShop_Oregano.Models.ViewModel;
using MovieShop_Oregano.Services;
using System.Diagnostics;

namespace MovieShop_Oregano.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
		private readonly IMovieService _movieService;
		public HomeController(ILogger<HomeController> logger, IMovieService movieService)
        {
            _logger = logger;
            _movieService = movieService;
        }

        public IActionResult Index()
        {

            FrontDashboardVM frontPage = new FrontDashboardVM()
            {
                NewestMovies = _movieService.TopFiveNewestMovies(),
                OldestMovies = _movieService.TopFiveOldestMovies(),
                CheapestMovies = _movieService.TopFiveCheapestMovies(),
                MostPopularMovie = _movieService.MostPopularMovie(),
                MostExpensiveOrder = _movieService.MostExpensiveOrder()
            };
            

            return View(frontPage);
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
