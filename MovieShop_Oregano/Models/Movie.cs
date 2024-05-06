using System.ComponentModel.DataAnnotations;

namespace MovieShop_Oregano.Models
{
    public class Movie
    {

        public int Id { get; set; }

        [Required]
        [Display(Name = "Movie Title")]
        public string Title { get; set; }
        
        [Required]
        public string Director { get; set; }

        [Required]
        public int ReleaseYear { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        
        [DataType(DataType.ImageUrl)]
        public string MovieImg { get; set; }

        public virtual ICollection<OrderRow> OrderRows { get; set; } = new List<OrderRow>();

        public int Qty { get; set; }

        public Movie()
        {

        }
        public Movie(int id, string title, string director, int releaseYear, decimal price, string movieImg, int qty)
        {
            Id = id;
            Title = title;
            Director = director;
            ReleaseYear = releaseYear;
            Price = price;
            MovieImg = movieImg;
            Qty = qty;
        }

    }
}
