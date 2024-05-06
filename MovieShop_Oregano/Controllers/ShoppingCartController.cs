using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieShop_Oregano.Models;
using MovieShop_Oregano.Models.ViewModel;
using MovieShop_Oregano.Services;
using Newtonsoft.Json;
using System.IO;

namespace MovieShop_Oregano.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly ILogger<ShoppingCartController> _logger;

        private readonly IMovieService _movieService;
        public ShoppingCartController(ILogger<ShoppingCartController> logger, IMovieService movieService)
        {
            _logger = logger;
            _movieService = movieService;
        }

        public IActionResult Index()
        {
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
                model.Qty= quantity;

                MovieL.Add(model);
            }

            return View(MovieL);
        }
        
        public IActionResult AddToCart(int id, int qtyCount)
        {
            // Retrieve existing session data if any
            var sessionDataJson = HttpContext.Session.GetString("CartData");
            Dictionary<int, int> sessionData = sessionDataJson != null
                                               ? JsonConvert.DeserializeObject<Dictionary<int, int>>(sessionDataJson)
                                               : new Dictionary<int, int>();

            // Add or update the key-value pair
            sessionData[id] = qtyCount;

            // Serialize the updated session data back to JSON
            var updatedSessionDataJson = JsonConvert.SerializeObject(sessionData);

            // Save the updated session data
            HttpContext.Session.SetString("CartData", updatedSessionDataJson);
           
            // Optionally, return something indicating success
            return RedirectToAction("List", "Movie");
        }

        public IActionResult UpdateShopingCartRows(int id, int qtyCount, bool isDelete)
        {
            var sessionDataJson = HttpContext.Session.GetString("CartData");
            Dictionary<int, int> sessionData = sessionDataJson != null
                                               ? JsonConvert.DeserializeObject<Dictionary<int, int>>(sessionDataJson)
                                               : new Dictionary<int, int>();

            if (isDelete)
            {
                // If isDelete is true, remove the item from the session data
                if (sessionData.ContainsKey(id))
                {
                    sessionData.Remove(id);
                }

                var updatedSessionDataJson = JsonConvert.SerializeObject(sessionData);

                // Save the updated session data
                HttpContext.Session.SetString("CartData", updatedSessionDataJson);
            }
            else
            {
                // Add or update the key-value pair
                sessionData[id] = qtyCount;

                // Serialize the updated session data back to JSON
                var updatedSessionDataJson = JsonConvert.SerializeObject(sessionData);

                // Save the updated session data
                HttpContext.Session.SetString("CartData", updatedSessionDataJson);
            }
            
            // Optionally, return something indicating success
            return RedirectToAction("List", "Movie");
        }

        public IActionResult UpdateItemCount(int itemCount)
        {
            int exItemCount = Convert.ToInt32(HttpContext.Session.GetInt32("CartItemCount"));
            // Update the session variable with the item count
            HttpContext.Session.SetInt32("CartItemCount", exItemCount + itemCount);

            return Json(new { success = true });
        }

        public IActionResult GetCartItemCount()
        {
            int itemCount = Convert.ToInt32(HttpContext.Session.GetInt32("CartItemCount"));
           
            return Json(new { itemCount = itemCount });
        }

        public IActionResult RemoveCartItemCount(int count)
        {
            int itemCount = Convert.ToInt32(HttpContext.Session.GetInt32("CartItemCount"));

            HttpContext.Session.SetInt32("CartItemCount", itemCount - count);

            return Json(new { success = true });
        }

        public IActionResult RemoveAndUpdateCartItemCount(int count)
        {
            HttpContext.Session.SetInt32("CartItemCount", count);

            return Json(new { success = true });
        }

    }
}
