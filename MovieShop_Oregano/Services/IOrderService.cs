using MovieShop_Oregano.Models;

namespace MovieShop_Oregano.Services
{
    public interface IOrderService
    {
        List<Order> GetOrders();
        Order GetOrderById(int id);
        Order AddOrder(Order order);
        public void AddOrdeRow(OrderRow order);
        Order GetLatestOrder();
    }
}
