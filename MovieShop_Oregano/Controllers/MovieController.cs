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
        private readonly MovieDbContext _context;
        public MovieController(IMovieService movieService, MovieDbContext context)
		{
			_movieService = movieService;
			_context = context;
		}
      
        public IActionResult Index(int pg=1, string lastSearchOption = null)
        {
			var movie = _movieService.GetMovies();

			const int pageSize = 6;
			if (pg < 1)
				pg = 1;

			int recCount = movie.Count();
			var pager = new Pager(recCount, pg, pageSize);
			int recSkip = (pg - 1) * pageSize;
			var data = movie.Skip(recSkip).Take(pager.PageSize).ToList();
			this.ViewBag.Pager = pager;

			ViewBag.LastSearchOption = lastSearchOption;

			return View(data);
        }

		[HttpPost]
		public IActionResult Search(string searchOption, string searchInput)
		{

			ViewBag.LastSearchOption = searchOption;
			ViewBag.LastSearchInput = searchInput;

			if (string.IsNullOrEmpty(searchOption) || string.IsNullOrEmpty(searchInput))
			{
				var movies = _movieService.GetMovies();
				return View("Index", movies);
			}
			else
			{
				var movies = _movieService.SearchMovies(searchOption, searchInput);
				return View("Index", movies);
			}
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
        public IActionResult List(int? number, string? txt)
        {
           // HttpContext.Session.Clear();
            MovieVM model = new MovieVM();
			if(txt == null)
				model.MovieList = _movieService.GetMovies();
			else
			{
                model.MovieList = _context.Movies.Where(x => x.Title  == txt).ToList();
            }
            return View(model);
        }

        public IActionResult SearchList(int number, string txt) {//not use
            // HttpContext.Session.Clear();
            MovieVM model = new MovieVM();
			model.MovieList = _context.Movies.OrderByDescending(x => x.Title == txt).ToList();
			return RedirectToAction("List", "");
			return View(model);
        }
    }
}
