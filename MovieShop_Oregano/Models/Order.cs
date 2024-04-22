using System.ComponentModel.DataAnnotations;

namespace MovieShop_Oregano.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy")]
        public DateTime OrderDate { get; set; }

        public virtual int CustomerId { get; set; }
        public virtual ICollection<OrderRow> OrderRows { get; set; } = new List<OrderRow>();

    }
}
