using Microsoft.EntityFrameworkCore;

namespace MovieShop_Oregano.Data
{
    public class MovieDbContext : DbContext
    {
        public MovieDbContext(DbContextOptions<MovieDbContext> options) : base(options)
        {

        }

    }
}
