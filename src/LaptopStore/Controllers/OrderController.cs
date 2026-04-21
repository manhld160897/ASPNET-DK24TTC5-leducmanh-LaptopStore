using LaptopStore.Models;
using LaptopStore.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LaptopStore.Controllers
{
    public class OrderController : Controller
    {
        private readonly DataContext _context;

        public OrderController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Checkout()
        {
            var customerId = HttpContext.Session.GetInt32("CustomerId");
            if (customerId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var cartItems = _context.CartItems
                .Include(c => c.Product)
                .Where(c => c.CustomerId == customerId.Value)
                .ToList();

            if (!cartItems.Any())
            {
                return RedirectToAction("Index", "Cart");
            }

            return View(new CheckoutViewModel());
        }

        [HttpPost]
        public IActionResult Checkout(CheckoutViewModel model)
        {
            var customerId = HttpContext.Session.GetInt32("CustomerId");
            if (customerId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var cartItems = _context.CartItems
                .Include(c => c.Product)
                .Where(c => c.CustomerId == customerId.Value)
                .ToList();

            if (!cartItems.Any())
            {
                return RedirectToAction("Index", "Cart");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            decimal totalAmount = cartItems.Sum(item => item.Product.Price * item.Quantity);

            var order = new Order
            {
                CustomerId = customerId.Value,
                OrderDate = DateTime.Now,
                TotalAmount = totalAmount,
                Status = "Pending",
                ReceiverName = model.ReceiverName,
                ReceiverPhone = model.ReceiverPhone,
                ShippingAddress = model.ShippingAddress
            };

            _context.Orders.Add(order);
            _context.SaveChanges();

            foreach (var item in cartItems)
            {
                var orderDetail = new OrderDetail
                {
                    OrderId = order.OrderId,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = item.Product.Price
                };

                _context.OrderDetails.Add(orderDetail);
            }

            _context.SaveChanges();

            _context.CartItems.RemoveRange(cartItems);
            _context.SaveChanges();

            return RedirectToAction("MyOrders");
        }

        public IActionResult MyOrders()
        {
            var customerId = HttpContext.Session.GetInt32("CustomerId");
            if (customerId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var orders = _context.Orders
                .Where(o => o.CustomerId == customerId.Value)
                .OrderByDescending(o => o.OrderDate)
                .ToList();

            return View(orders);
        }

        public IActionResult Details(int id)
        {
            var customerId = HttpContext.Session.GetInt32("CustomerId");
            if (customerId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var order = _context.Orders
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                .FirstOrDefault(o => o.OrderId == id && o.CustomerId == customerId.Value);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }
    }
}