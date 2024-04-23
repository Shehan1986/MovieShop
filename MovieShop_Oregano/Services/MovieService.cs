using MovieShop_Oregano.Data;
using MovieShop_Oregano.Models;

namespace MovieShop_Oregano.Services
{
    public class MovieService : IMovieService
    {
        private readonly MovieDbContext _db;
        private readonly IConfiguration _configuration;

        public MovieService(MovieDbContext db, IConfiguration configuration)
        {
            _db = db;
            _configuration = configuration;
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
            var topMostPopularMovie = _db.Movies.OrderByDescending(m => m.OrderRows.Count).FirstOrDefault();                                                                          
            return topMostPopularMovie;
        }

        public void AddMovie(Movie movie)
        {
            _db.Movies.Add(movie);
            _db.SaveChanges();
        }


    }
}
