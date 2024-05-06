using Microsoft.AspNetCore.Mvc;
using MovieShop_Oregano.Data;
using MovieShop_Oregano.Models;
using MovieShop_Oregano.Models.ViewModel;
using MovieShop_Oregano.Services;
using Newtonsoft.Json;

namespace MovieShop_Oregano.Controllers
{
    public class OrderController : Controller
    {
        private readonly IMovieService _movieService;
        private readonly IOrderService _orderService;

        public OrderController(IMovieService movieService, IOrderService orderService)
        {
            _movieService = movieService;
            _orderService = orderService;
        }
        public IActionResult Index()
        {
            
            return View();
        }

        public IActionResult OrderConfirmation()
        {
            ViewBag.CustomerAddress = Convert.ToString(HttpContext.Session.GetString("Customer_Address"));
            var sessionDataJson = HttpContext.Session.GetString("CartData");
            Dictionary<int, int> sessionData = sessionDataJson != null
                                               ? JsonConvert.DeserializeObject<Dictionary<int, int>>(sessionDataJson)
                                               : new Dictionary<int, int>();

            List<Movie> MovieL = new List<Movie>();
            foreach (var kvp in sessionData)
            {
                int movieId = kvp.Key;
                int quantity = kvp.Value;

                Movie model = _movieService.GetMovieById(movieId);
                model.Qty = quantity;

                MovieL.Add(model);
            }

            return View(MovieL);
        }

        public IActionResult SessionClear()
        {
            var sessionDataJson = HttpContext.Session.GetString("CartData");
             var customer_ID = HttpContext.Session.GetInt32("Customer_ID");

            Dictionary<int, int> sessionData = sessionDataJson != null
                                              ? JsonConvert.DeserializeObject<Dictionary<int, int>>(sessionDataJson)
                                              : new Dictionary<int, int>();
            
            Order ObjOrder = new Order();
            ObjOrder.OrderDate = DateTime.Now;
            ObjOrder.CustomerId = Convert.ToInt32(customer_ID);
            _orderService.AddOrder(ObjOrder);
            
            var orderObj = _orderService.GetLatestOrder();

            int orderID = orderObj.Id;

            
            foreach (var kvp in sessionData)
            {
                OrderRow ObjrderRow = new OrderRow();

                int movieId = kvp.Key;
                int quantity = kvp.Value;

                var objMovie = _movieService.GetMovieById(movieId);
                

                ObjrderRow.Price = objMovie.Price;
                ObjrderRow.OrderId= Convert.ToInt32(orderID);
                ObjrderRow.MovieId = Convert.ToInt32(movieId);
                ObjrderRow.Qty = quantity;
                ObjrderRow.Movie = null;
                ObjrderRow.Order = null;

                _orderService.AddOrdeRow(ObjrderRow);
            }

            HttpContext.Session.Clear();

            return RedirectToAction("Index","Home");
            //return Json(new { success = true });
        }
    }
}
