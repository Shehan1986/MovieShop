using Microsoft.AspNetCore.Mvc;
using MovieShop_Oregano.Models;
using MovieShop_Oregano.Services;

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
	}
}
