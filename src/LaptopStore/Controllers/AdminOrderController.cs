using LaptopStore.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LaptopStore.Controllers
{
    public class AdminOrderController : Controller
    {
        private readonly DataContext _context;

        public AdminOrderController(DataContext context)
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

            var orders = _context.Orders
                .Include(o => o.Customer)
                .OrderByDescending(o => o.OrderDate)
                .ToList();

            return View(orders);
        }

        public IActionResult Details(int id)
        {
            if (!CheckAdmin()) return RedirectToAction("Login", "Account");

            var order = _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                .FirstOrDefault(o => o.OrderId == id);

            if (order == null) return NotFound();

            return View(order);
        }

        [HttpPost]
        public IActionResult UpdateStatus(int orderId, string status)
        {
            if (!CheckAdmin()) return RedirectToAction("Login", "Account");

            var order = _context.Orders.FirstOrDefault(o => o.OrderId == orderId);
            if (order != null)
            {
                order.Status = status;
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}