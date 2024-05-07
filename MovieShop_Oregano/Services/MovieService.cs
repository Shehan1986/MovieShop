using Azure;
using MovieShop_Oregano.Data;
using MovieShop_Oregano.Models;
using System.Net;
using System.Text;

namespace MovieShop_Oregano.Services
{
    public class MovieService : IMovieService
    {
        private readonly MovieDbContext _db;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public MovieService(MovieDbContext db, IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
        }

        public List<Movie> GetMovies()
        {
            var movieList = _db.Movies.ToList();
            return movieList;
        }
        public Movie GetMovieById(int id)
        {
            var movieId = _db.Movies.FirstOrDefault(m => m.Id == id);
            return movieId;
        }

        public List<Movie> TopFiveNewestMovies()
        {
            var topFiveMoviesByNewest = _db.Movies.OrderByDescending(m => m.ReleaseYear).Take(5).ToList();
            return topFiveMoviesByNewest;
        }
        public List<Movie> TopFiveOldestMovies()
        {
            var topFiveMoviesByOldest = _db.Movies.OrderBy(m => m.ReleaseYear).Take(5).ToList();
            return topFiveMoviesByOldest;
        }
        public List<Movie> TopFiveCheapestMovies()
        {
            var topFiveMovieByCheapest = _db.Movies.OrderBy(m => m.Price).Take(5).ToList();
            return topFiveMovieByCheapest;
        }
        public Movie MostPopularMovie()
        {
            var mostPopularMovie = _db.Movies.OrderByDescending(m => m.OrderRows.Count).FirstOrDefault();                                                                          
            return mostPopularMovie;
        }

        public Customer MostExpensiveOrder()
        {
            var mostExpensiveOrder = _db.Customers.OrderByDescending(customer => customer.Orders
                .SelectMany(order => order.OrderRows)
                .Sum(orderRow => orderRow.Price)).FirstOrDefault();

			return mostExpensiveOrder;
        }

        public void AddMovie(Movie movie)
        {
            _db.Movies.Add(movie);
            _db.SaveChanges();
        }
        public void UpdateMovie(Movie movie)
        {
            _db.Update(movie);
            _db.SaveChanges();
        }

        public void DeleteMovie(Movie movie)
        {
            _db.Remove(movie);
            _db.SaveChanges();
        }

		public List<Movie> SearchMovies(string searchOption, string searchInput)
		{
			var query = _db.Movies.AsQueryable();

			switch (searchOption)
			{
				case "Director":
					query = query.Where(m => m.Director.Contains(searchInput));
					break;
				case "Title":
					query = query.Where(m => m.Title.Contains(searchInput));
					break;
				case "ReleaseYear":
					if (int.TryParse(searchInput, out int releaseYear))
					{
						query = query.Where(m => m.ReleaseYear == releaseYear);
					}
					break;
				default:
					query = query.Where(m => m.Director.Contains(searchInput));
					break;
			}

			query = query.OrderByDescending(m => m.Id);

			return query.ToList();

		}

		public async Task<string> SaveImageFromUrl(string imageUrl, string movieTitle)
		{
			try
			{
				var invalidChars = Path.GetInvalidFileNameChars();
				var sanitizedTitle = new StringBuilder();

				foreach (var character in movieTitle)
				{
					if (!invalidChars.Contains(character) && character != ':')
					{
						sanitizedTitle.Append(character);
					}
					else if (character == ' ' || character == '-')
					{
						sanitizedTitle.Append(character);
					}
				}

				using (var httpClient = new HttpClient())
				{
					var response = await httpClient.GetAsync(imageUrl);                   
					if (response.IsSuccessStatusCode)
					{
						var fileName = $"{sanitizedTitle}.jpg"; // Use the sanitized movie title for the file name
						var savePath = Path.Combine(_webHostEnvironment.WebRootPath, "img", fileName);
						using (var stream = await response.Content.ReadAsStreamAsync())
						{
							using (var fileStream = new FileStream(savePath, FileMode.Create))
							{
								await stream.CopyToAsync(fileStream);
							}
						}
						return fileName;
					}
					else
					{
						throw new Exception($"Failed to download image from {imageUrl}. Status code: {response.StatusCode}");
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error downloading image from {imageUrl}: {ex.Message}");

				return $"Error downloading image: {ex.Message}";
			}
		}

		public void DeleteImage(string fileName)
        {
            var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "img", fileName);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

    }
}
