using MovieShop_Oregano.Models;

namespace MovieShop_Oregano.Services
{
    public interface IMovieService
    {
        
        List<Movie> GetMovies();
        Movie GetMovieById(int id);

        List<Movie> TopFiveNewestMovies();
        List<Movie> TopFiveOldestMovies();
        List<Movie> TopFiveCheapestMovies();
        List<Movie> MostPopularMovies();

        void AddMovie(Movie movie);
    }
}
