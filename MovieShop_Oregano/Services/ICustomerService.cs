using MovieShop_Oregano.Models;

namespace MovieShop_Oregano.Services
{
    public interface ICustomerService
    {
        List<Customer> GetCustomer();
        Customer GetCustomerById(int id);
        Customer GetCustomerByEmail(string email);
        public void AddCustomer(Customer customer);

        public void UpdateCustomer(Customer customer);
    }
}
