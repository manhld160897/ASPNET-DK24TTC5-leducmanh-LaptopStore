using LaptopStore.Models;
using LaptopStore.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LaptopStore.Controllers
{
    public class CartController : Controller
    {
        private readonly DataContext _context;

        public CartController(DataContext context)
        {
            _context = context;
        }

        public IActionResult Index()
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

            return View(cartItems);
        }

        [HttpPost]
        [HttpPost]
        public IActionResult AddToCart(int productId, int quantity = 1)
        {
            var customerId = HttpContext.Session.GetInt32("CustomerId");
            if (customerId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var product = _context.Products.FirstOrDefault(p => p.ProductId == productId);
            if (product == null)
            {
                return NotFound();
            }

            if (quantity < 1)
            {
                quantity = 1;
            }

            var cartItem = _context.CartItems.FirstOrDefault(c =>
                c.CustomerId == customerId.Value && c.ProductId == productId);

            if (cartItem == null)
            {
                cartItem = new CartItem
                {
                    CustomerId = customerId.Value,
                    ProductId = productId,
                    Quantity = quantity
                };

                _context.CartItems.Add(cartItem);
            }
            else
            {
                cartItem.Quantity += quantity;
                _context.CartItems.Update(cartItem);
            }

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Increase(int id)
        {
            var cartItem = _context.CartItems.FirstOrDefault(c => c.CartItemId == id);
            if (cartItem != null)
            {
                cartItem.Quantity += 1;
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public IActionResult Decrease(int id)
        {
            var cartItem = _context.CartItems.FirstOrDefault(c => c.CartItemId == id);
            if (cartItem != null)
            {
                cartItem.Quantity -= 1;

                if (cartItem.Quantity <= 0)
                {
                    _context.CartItems.Remove(cartItem);
                }

                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public IActionResult Remove(int id)
        {
            var cartItem = _context.CartItems.FirstOrDefault(c => c.CartItemId == id);
            if (cartItem != null)
            {
                _context.CartItems.Remove(cartItem);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}