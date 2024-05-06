namespace MovieShop_Oregano.Models.ViewModel
{
    public class FrontDashboardVM
    {
        public List<Movie> NewestMovies { get; set; }
        public List<Movie> OldestMovies { get; set; }
        public List<Movie> CheapestMovies { get; set; }
        public Movie MostPopularMovie { get; set; }
        public Customer MostExpensiveOrder { get; set; }

    }
}
