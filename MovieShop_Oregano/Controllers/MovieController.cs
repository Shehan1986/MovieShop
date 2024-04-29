using Microsoft.AspNetCore.Mvc;
using MovieShop_Oregano.Data;
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
			var movie = _movieService.GetMovies();
			return View(movie);
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

		public IActionResult Edit(int id)
		{
			
			var movie =	_movieService.GetMovieById(id);
			
			
			return View(movie);
		}

		[HttpPost]
		public IActionResult Edit(int id, Movie movie)
		{

			/*if (id ! == movie.Id)
			{
				return NotFound();
			}*/

			
			if (ModelState.IsValid)
			{
				_movieService.UpdateMovie(movie);
				return RedirectToAction("Index");
			}

			return View(movie);
		}

		public IActionResult Delete(int id)
		{

			var movie = _movieService.GetMovieById(id);
			

			return View(movie);
		}

		[HttpPost]
		public IActionResult Delete(Movie movie)
		{
			if (!ModelState.IsValid)
			{
				_movieService.DeleteMovie(movie);
				return RedirectToAction("Index");
			}
			
			return View(movie);
		}

		public IActionResult Details(int id)
		{
			 var movie = _movieService.GetMovieById(id);
			return View(movie);
		}

		


	}
}
