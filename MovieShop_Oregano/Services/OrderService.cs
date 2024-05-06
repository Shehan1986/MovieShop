using MovieShop_Oregano.Data;
using MovieShop_Oregano.Models;

namespace MovieShop_Oregano.Services
{
    public class OrderService:IOrderService
    {
        private readonly MovieDbContext _db;
        private readonly IConfiguration _configuration;

        public OrderService(MovieDbContext db, IConfiguration configuration)
        {
            _db = db;
            _configuration = configuration;
        }
        public List<Order> GetOrders()
        {
            var orderList = _db.Orders.ToList();
            return orderList;
        }
        public Order GetOrderById(int id)
        {
            var order = _db.Orders.FirstOrDefault(m => m.Id == id);
            return order;
        }
        public Order AddOrder(Order order)
        {
            _db.Orders.Add(order);
            _db.SaveChanges();

            var Objorder = _db.Orders.FirstOrDefault();
            return Objorder;
        }

        public void AddOrdeRow(OrderRow order)
        {
            _db.OrderRows.Add(order);
            _db.SaveChanges();

        }
        public Order GetLatestOrder()
        {
            var order = _db.Orders.OrderByDescending(m => m.Id).FirstOrDefault();
            
            return order;
        }

        
    }
}
