using Microsoft.AspNetCore.Mvc;
using MovieShop_Oregano.Data;
using MovieShop_Oregano.Models;
using MovieShop_Oregano.Models.ViewModel;
using MovieShop_Oregano.Services;
using Newtonsoft.Json;
using System.Net;

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
		public async Task<IActionResult> Create(Movie movie)
		{
			if (ModelState.IsValid)
			{
				movie.MovieImg = await _movieService.SaveImageFromUrl(movie.MovieImg, movie.Title);
								
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
		public async Task<IActionResult> Edit(int id, Movie movie)
		{

			if (id != movie.Id)
			{
				return NotFound();
			}
			
			if (ModelState.IsValid)
			{
				movie.MovieImg = await _movieService.SaveImageFromUrl(movie.MovieImg, movie.Title);

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

		[HttpPost, ActionName("Delete")]
		public IActionResult DeleteConfirmed(Movie movie)
		{

			if (ModelState.IsValid)
			{
				_movieService.DeleteMovie(movie);
				_movieService.DeleteImage(movie.MovieImg);
				return RedirectToAction("Index");
			}

			return View(movie);
		}

		public IActionResult Details(int id)
		{
			 var movie = _movieService.GetMovieById(id);
			return View(movie);
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
