using Microsoft.EntityFrameworkCore;
using MovieShop_Oregano.Models;

namespace MovieShop_Oregano.Data
{
    public class MovieDbContext : DbContext
    {
        public MovieDbContext(DbContextOptions<MovieDbContext> options) : base(options)
        {

        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<OrderRow> OrderRows { get; set; }
    }
}
