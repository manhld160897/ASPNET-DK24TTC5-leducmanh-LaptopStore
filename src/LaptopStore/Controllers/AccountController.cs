using LaptopStore.Models;
using LaptopStore.Repository;
using Microsoft.AspNetCore.Mvc;

namespace LaptopStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly DataContext _context;

        public AccountController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Customer customer)
        {
            var user = _context.Customers.FirstOrDefault(c =>
                c.Email == customer.Email && c.Password == customer.Password);

            if (user != null)
            {
                HttpContext.Session.SetInt32("CustomerId", user.CustomerId);
                HttpContext.Session.SetString("CustomerName", user.FullName);
                HttpContext.Session.SetString("CustomerEmail", user.Email);
                HttpContext.Session.SetString("Role", user.Role);

                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "Email hoặc mật khẩu không đúng";
            return View(customer);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(Customer customer)
        {
            if (ModelState.IsValid)
            {
                var checkEmail = _context.Customers.FirstOrDefault(c => c.Email == customer.Email);
                if (checkEmail != null)
                {
                    ViewBag.Error = "Email đã tồn tại";
                    return View(customer);
                }

                _context.Customers.Add(customer);
                _context.SaveChanges();

                return RedirectToAction("Login");
            }

            return View(customer);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}