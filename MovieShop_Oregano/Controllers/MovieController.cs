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
       /* private string FetchAndSaveImage(string imageUrl, string movieTitle)
        {
			try
			{
				string fileName = $"{movieTitle.Replace(" ", "_").Replace("/", "_")}.jpg";
				string imagePath = Path.Combine("wwwroot", "img", fileName);

				using (Client webClient = new WebClient())
				{
					webClient.DownloadFile(new Uri(imageUrl), imagePath);
				}

				return Path.Combine("img", fileName);

            }
			catch (Exception ex)
			{
				Console.WriteLine($"Error fetching and saving image: {ex.Message}");
				return null;
			}
        }

        private void DeleteImage(string imagePath)
        {
			try
			{
				string fullPath = Path.Combine("wwwroot", imagePath);
                if (File.Exists(fullPath))
                {
                    
                    File.Delete(fullPath);
                }

            }
			catch (Exception ex)
			{
				Console.WriteLine($"Error deleting imamge: {ex.Message}");
			}
        }*/

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
				if (!string.IsNullOrEmpty(movie.MovieImg))
				{
					var imageUrl = movie.MovieImg;
					var fileName = await _movieService.SaveImageFromUrl(movie.MovieImg, movie.Title);
					if (!string.IsNullOrEmpty(fileName))
					{
						movie.MovieImg = fileName;
					}
				}
								
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
			if (ModelState.IsValid)
			{
				if (!string.IsNullOrEmpty(movie.MovieImg))
				{
					var imageUrl = movie.MovieImg;
					var fileName = await _movieService.SaveImageFromUrl(movie.MovieImg, movie.Title);
					if (!string.IsNullOrEmpty(fileName))
					{
						_movieService.DeleteImage(movie.MovieImg);
						movie.MovieImg = fileName;
					}
				}					
				
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
			var movieToDelete = _movieService.GetMovieById(movie.Id);

			if (movieToDelete != null)
			{
				_movieService.DeleteMovie(movieToDelete);
				_movieService.DeleteImage(movieToDelete.MovieImg);
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
