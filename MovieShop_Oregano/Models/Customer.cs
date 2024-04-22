using System.ComponentModel.DataAnnotations;

namespace MovieShop_Oregano.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Billing Adress")]
        public string BillingAddress { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Billing City")]
        public string BillingCitys { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = "Postal Code")]
        public string BillingZip { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Delivery Adress")]
        public string DeliveryAdress { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Delivery City")]
        public string DeliveryCity { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = "Postal Code")]
        public string DeliveryZip { get; set; }

        [Required]
        [StringLength(20)]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone")]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(200)]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
