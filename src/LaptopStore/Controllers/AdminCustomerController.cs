using LaptopStore.Repository;
using Microsoft.AspNetCore.Mvc;

namespace LaptopStore.Controllers
{
    public class AdminCustomerController : Controller
    {
        private readonly DataContext _context;

        public AdminCustomerController(DataContext context)
        {
            _context = context;
        }

        private bool CheckAdmin()
        {
            return HttpContext.Session.GetString("Role") == "Admin";
        }

        public IActionResult Index()
        {
            if (!CheckAdmin()) return RedirectToAction("Login", "Account");

            var customers = _context.Customers.ToList();
            return View(customers);
        }

        public IActionResult Delete(int id)
        {
            if (!CheckAdmin()) return RedirectToAction("Login", "Account");

            var customer = _context.Customers.Find(id);
            if (customer == null)
            {
                return RedirectToAction("Index");
            }

            if (customer.Role == "Admin")
            {
                TempData["Error"] = "Không thể xóa tài khoản Admin.";
                return RedirectToAction("Index");
            }

            _context.Customers.Remove(customer);
            _context.SaveChanges();

            TempData["Success"] = "Xóa khách hàng thành công.";
            return RedirectToAction("Index");
        }
    }
}