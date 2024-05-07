using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using MovieShop_Oregano.Data;
using MovieShop_Oregano.Models;
using MovieShop_Oregano.Models.ViewModel;
using static NuGet.Packaging.PackagingConstants;

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

        public List<CustomerOrderHeader> GetCustomerOrders()
        {
            List<CustomerOrderHeader> customerOrderHeaders = new List<CustomerOrderHeader>();

            var query = _db.Customers
                             .Join(_db.Orders, cust => cust.Id, ord => ord.CustomerId, (cust, ord) => new { Customer = cust, Order = ord })
                             .Join(_db.OrderRows, join1 => join1.Order.Id, row => row.OrderId, (join1, row) => new { join1.Customer, join1.Order, OrderRow = row })
                             .Join(_db.Movies, join2 => join2.OrderRow.MovieId, mov => mov.Id, (join2, mov) => new { join2.Customer, join2.Order, join2.OrderRow, Movie = mov })
                             .Select(result => new
                             {
                                 CustomerName = $"{result.Customer.FirstName} {result.Customer.LastName}",
                                 Address = $"{result.Customer.DeliveryAdress}, {result.Customer.DeliveryZip}, {result.Customer.DeliveryCity}",
                                 Email = result.Customer.Email,
                                 Phone = result.Customer.PhoneNumber,
                                 OrderId = result.Order.Id,
                                 OrderDate = result.Order.OrderDate,
                                 MovieTitle = result.Movie.Title,
                                 MovieImage = result.Movie.MovieImg,
                                 Price = result.OrderRow.Price,
                                 Quantity = result.OrderRow.Qty,
                                 TotalPrice = result.OrderRow.Price * result.OrderRow.Qty
                             })
                             .GroupBy(result => result.OrderId)
                             .Select(group => new
                             {
                                 OrderId = group.Key,
                                 CustomerName = group.First().CustomerName,
                                 Address = group.First().Address,
                                 Email = group.First().Email,
                                 Phone = group.First().Phone,
                                 OrderDate = group.First().OrderDate,
                                 OrderTotalPrice = group.Sum(item => item.TotalPrice),
                                 OrderDetails = group.Select(item => new
                                 {
                                     MovieTitle = item.MovieTitle,
                                     MovieImage = item.MovieImage,
                                     Price = item.Price,
                                     Quantity = item.Quantity,
                                     TotalPrice = item.TotalPrice
                                 })
                             });


            // Initialize a list to store CustomerOrderHeader objects


            // Iterate over each group in the grouped query result
            foreach (var group in query)
            {
                // Create a new CustomerOrderHeader object
                CustomerOrderHeader customerOrderHeader = new CustomerOrderHeader
                {
                    // Populate properties from the group
                    CustomerName = group.CustomerName,
                    CustomerAddress = group.Address,
                    Email = group.Email,
                    Phone = group.Phone,
                    OrderDate = group.OrderDate,
                    SubTotal = group.OrderTotalPrice,
                    CustomerOrderDetailsList = new List<CustomerOrderDetails>()
                };

                // Iterate over each item in the group's order details and add them to CustomerOrderDetailsList
                foreach (var orderDetail in group.OrderDetails)
                {
                    customerOrderHeader.CustomerOrderDetailsList.Add(new CustomerOrderDetails
                    {
                        MovieTitle = orderDetail.MovieTitle,
                        MovieImage = orderDetail.MovieImage,
                        Price = orderDetail.Price,
                        OrderQTY = orderDetail.Quantity,
                        TotalPrice = orderDetail.TotalPrice
                    });
                }

                // Add the created CustomerOrderHeader object to the list
                customerOrderHeaders.Add(customerOrderHeader);
            }


            return customerOrderHeaders;
        }


    }
}
