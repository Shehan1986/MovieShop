using MovieShop_Oregano.Models;
using MovieShop_Oregano.Models.ViewModel;

namespace MovieShop_Oregano.Services
{
    public interface IOrderService
    {
        List<Order> GetOrders();
        Order GetOrderById(int id);
        Order AddOrder(Order order);
        public void AddOrdeRow(OrderRow order);
        Order GetLatestOrder();
        List<CustomerOrderHeader> GetCustomerOrders();
    }
}
