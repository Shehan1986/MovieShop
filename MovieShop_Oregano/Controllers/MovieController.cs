using Microsoft.AspNetCore.Mvc;
using MovieShop_Oregano.Models;
using MovieShop_Oregano.Models.ViewModel;
using MovieShop_Oregano.Services;
using Newtonsoft.Json;

namespace MovieShop_Oregano.Controllers
{
    public class MovieController : Controller
    {
		private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
		{
			_movieService = movieService;
		}

		public IActionResult Index()
        {
            return View();
        }

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Create(Movie movie)
		{
			if (ModelState.IsValid)
			{
				_movieService.AddMovie(movie);
				return RedirectToAction("Index");
			}
			
			return View(movie);
		}

		public IActionResult Edit()
		{
			
			
			return View();
		}

		public IActionResult Delete()
		{
			return View();
		}

		public IActionResult Details()
		{
			return View();
		}

        public IActionResult List()
        {
           // HttpContext.Session.Clear();
            MovieVM model = new MovieVM();
            model.MovieList = _movieService.GetMovies();
            return View(model);
        }
    }
}
