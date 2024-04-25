using MovieShop_Oregano.Data;
using MovieShop_Oregano.Models;

namespace MovieShop_Oregano.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly MovieDbContext _db;
        private readonly IConfiguration _configuration;
        public CustomerService(MovieDbContext db, IConfiguration configuration)
        {
            _db = db;
            _configuration = configuration;
        }
        public List<Customer> GetCustomer()
        {
            var customer = _db.Customers.OrderBy(p => p.FirstName).ToList();
            return customer;
        }
        public Customer GetCustomerById(int id)
        {
            var customer = _db.Customers.FirstOrDefault(m => m.Id == id);
            return customer;
        }

        public Customer GetCustomerByEmail(string email)
        {
            var customer = _db.Customers.FirstOrDefault(m => m.Email == email);
            return customer;
        }

        public void AddCustomer(Customer customer)
        {
            _db.Customers.Add(customer);
            _db.SaveChanges();
        }
        public void UpdateCustomer(Customer customer)
        {
            _db.Customers.Update(customer);
            _db.SaveChanges();
        }

    }
}
