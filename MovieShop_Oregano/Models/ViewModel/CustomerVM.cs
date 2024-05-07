namespace MovieShop_Oregano.Models.ViewModel
{
    public class CustomerVM
    {
        public List<Customer> CustomerList { get; set; }

        public List<CustomerOrderHeader> CustomerOrderHeaderList { get; set; }
      
    }

    public class CustomerOrderHeader
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }

        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime OrderDate { get; set; }

        public decimal SubTotal { get; set; }

        public List<CustomerOrderDetails> CustomerOrderDetailsList { get; set; }

}
    public class CustomerOrderDetails
    { 
        public int MovieID { get; set; }
        public string MovieTitle { get; set; }
        public string MovieImage { get; set; }
        public decimal Price { get; set; }

        public int OrderID { get; set; }
        public int OrderQTY { get; set; }

        public decimal TotalPrice { get; set; }

    }
}
