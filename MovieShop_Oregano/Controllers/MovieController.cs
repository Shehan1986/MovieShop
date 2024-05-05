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

        List<Movie> MovieL = new List<Movie>()
        {
            new Movie(1,"Title", "Director", 2001,100,"https://via.placeholder.com/100"),
            new Movie(2,"Title", "Director", 2001,100,"https://via.placeholder.com/100"),
            new Movie(3,"Title", "Director", 2001,100,"https://via.placeholder.com/100"),
            new Movie(4,"Title", "Director", 2001,100,"https://via.placeholder.com/100"),
            new Movie(5,"Abc", "Shehan", 2001,100,"https://via.placeholder.com/100"),
            new Movie(6,"Title", "Director", 2001,100,"https://via.placeholder.com/100"),
            new Movie(1,"Title", "Director", 2001,100,"https://via.placeholder.com/100"),
            new Movie(2,"Title", "Director", 2001,100,"https://via.placeholder.com/100"),
            new Movie(3,"Title", "Director", 2001,100,"https://via.placeholder.com/100"),
            new Movie(4,"Title", "Director", 2001,100,"https://via.placeholder.com/100"),
            new Movie(5,"Abc", "Shehan", 2001,100,"https://via.placeholder.com/100"),
            new Movie(6,"Title", "Director", 2001,100,"https://via.placeholder.com/100"),
            new Movie(1,"Title", "Director", 2001,100,"https://via.placeholder.com/100"),
            new Movie(2,"Title", "Director", 2001,100,"https://via.placeholder.com/100"),
            new Movie(3,"Title", "Director", 2001,100,"https://via.placeholder.com/100"),
            new Movie(4,"Title", "Director", 2001,100,"https://via.placeholder.com/100"),
            new Movie(5,"Abc", "Shehan", 2001,100,"https://via.placeholder.com/100"),
            new Movie(6,"Title", "Director", 2001,100,"https://via.placeholder.com/100"),
            new Movie(1,"Title", "Director", 2001,100,"https://via.placeholder.com/100"),
            new Movie(2,"Title", "Director", 2001,100,"https://via.placeholder.com/100"),
            new Movie(3,"Title", "Director", 2001,100,"https://via.placeholder.com/100"),
            new Movie(4,"Title", "Director", 2001,100,"https://via.placeholder.com/100"),
            new Movie(5,"Abc", "Shehan", 2001,100,"https://via.placeholder.com/100"),
            new Movie(6,"Title", "Director", 2001,100,"https://via.placeholder.com/100"),
            new Movie(1,"Title", "Director", 2001,100,"https://via.placeholder.com/100"),
            new Movie(2,"Title", "Director", 2001,100,"https://via.placeholder.com/100"),
            new Movie(3,"Title", "Director", 2001,100,"https://via.placeholder.com/100"),
            new Movie(4,"Title", "Director", 2001,100,"https://via.placeholder.com/100"),
            new Movie(5,"Abc", "Shehan", 2001,100,"https://via.placeholder.com/100"),
            new Movie(6,"Title", "Director", 2001,100,"https://via.placeholder.com/100"),
            new Movie(1,"Title", "Director", 2001,100,"https://via.placeholder.com/100"),
            new Movie(2,"Title", "Director", 2001,100,"https://via.placeholder.com/100"),
            new Movie(3,"Title", "Director", 2001,100,"https://via.placeholder.com/100"),
            new Movie(4,"Title", "Director", 2001,100,"https://via.placeholder.com/100"),
            new Movie(5,"Abc", "Shehan", 2001,100,"https://via.placeholder.com/100"),
            new Movie(6,"Title", "Director", 2001,100,"https://via.placeholder.com/100")
        };
        public MovieController(IMovieService movieService)
		{
			_movieService = movieService;
		}
      
        public IActionResult Index(int pg=1)
        {
			var movie = _movieService.GetMovies();

			const int pageSize = 24;
			if (pg < 1)
				pg = 1;

			int recCount = movie.Count();
			var pager = new Pager(recCount, pg, pageSize);
			int recSkip = (pg - 1) * pageSize;
			var data = movie.Skip(recSkip).Take(pager.PageSize).ToList();
			this.ViewBag.Pager = pager;

			return View(data);
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
            MovieVM model = new MovieVM();
            model.MovieList = _movieService.GetMovies();
            return View(model);
        }
    }
}
