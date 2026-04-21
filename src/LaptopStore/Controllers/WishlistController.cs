using LaptopStore.Models;
using LaptopStore.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LaptopStore.Controllers
{
    public class WishlistController : Controller
    {
        private readonly DataContext _context;

        public WishlistController(DataContext context)
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

            var wishlistItems = _context.WishlistItems
                .Include(w => w.Product)
                .Where(w => w.CustomerId == customerId.Value)
                .ToList();

            return View(wishlistItems);
        }

        [HttpPost]
        public IActionResult Toggle(int productId)
        {
            var customerId = HttpContext.Session.GetInt32("CustomerId");
            if (customerId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var existed = _context.WishlistItems.FirstOrDefault(w =>
                w.CustomerId == customerId.Value && w.ProductId == productId);

            if (existed == null)
            {
                var item = new WishlistItem
                {
                    CustomerId = customerId.Value,
                    ProductId = productId
                };

                _context.WishlistItems.Add(item);
            }
            else
            {
                _context.WishlistItems.Remove(existed);
            }

            _context.SaveChanges();

            var referer = Request.Headers["Referer"].ToString();
            if (!string.IsNullOrEmpty(referer))
            {
                return Redirect(referer);
            }

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Remove(int id)
        {
            var customerId = HttpContext.Session.GetInt32("CustomerId");
            if (customerId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var item = _context.WishlistItems.FirstOrDefault(w =>
                w.WishlistItemId == id && w.CustomerId == customerId.Value);

            if (item != null)
            {
                _context.WishlistItems.Remove(item);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}