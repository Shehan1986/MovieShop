using Microsoft.AspNetCore.Mvc;
using MovieShop_Oregano.Data;
using MovieShop_Oregano.Models;
using MovieShop_Oregano.Models.ViewModel;
using MovieShop_Oregano.Services;

namespace MovieShop_Oregano.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly MovieDbContext _context;

        public CustomerController(ICustomerService customerService, MovieDbContext context)
        {
            _customerService = customerService;
            _context = context;
        }
        public IActionResult Index()
        {
            CustomerVM model = new CustomerVM();
            model.CustomerList = _customerService.GetCustomer();
            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Customer customer)
        {
            var extCustomer = _customerService.GetCustomerByEmail(customer.Email);
            if (ModelState.IsValid)
            {
                if(extCustomer != null)
                {
                    //customer.Id = extCustomer.Id;
                    //_customerService.UpdateCustomer(customer);
                    return RedirectToAction("OrderConfirmation", "Order");

                }
                else
                {
                    _customerService.AddCustomer(customer);
                    return RedirectToAction("OrderConfirmation", "Order");
                }
            }
            return View();
        }
        public IActionResult Edit(int id)
        {
            var customer = _customerService.GetCustomerById(id);

            return View(customer);
        }
        [HttpPost]
        public IActionResult Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                _context.Update(customer);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }
        public IActionResult Delete(int id)
        {
            var customer = _customerService.GetCustomerById(id);

            return View(customer);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var customer = _customerService.GetCustomerById(id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
            }

            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public ActionResult GetCustomerByEmail(string email)
        {
            try
            {
                var customer = _customerService.GetCustomerByEmail(email);
                if (customer != null)
                {
                    return Json(customer); 
                }
                else
                {
                    return Json(null); 
                }
            }
            catch (Exception ex)
            {
                return Json(null);
            }
        }

    }
}
