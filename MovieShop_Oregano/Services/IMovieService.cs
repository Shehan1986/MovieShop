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
        Movie MostPopularMovie();
        Customer MostExpensiveOrder();
        void AddMovie(Movie movie);

        void UpdateMovie(Movie movie);
        void DeleteMovie(Movie movie);
        /* Movie MovieDetails(int id);*/
        Task<string> SaveImageFromUrl(string imageUrl, string movieTitle);
        void DeleteImage(string fileName);
		List<Movie> SearchMovies(string searchOption, string searchInput);




	}
}
