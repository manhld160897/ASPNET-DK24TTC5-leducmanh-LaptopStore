using LaptopStore.Models;
using LaptopStore.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace LaptopStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataContext _context;

        public HomeController(DataContext context)
        {
            _context = context;
        }

        public IActionResult Index(string? keyword, int? categoryId, int page = 1)
        {
            int pageSize = 6;

            var query = _context.Products
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .AsQueryable();

            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(p => p.Name.Contains(keyword));
            }

            if (categoryId.HasValue)
            {
                query = query.Where(p => p.CategoryId == categoryId.Value);
            }

            query = query.OrderBy(p => p.ProductId);

            int totalProduct = query.Count();
            int totalPage = (int)Math.Ceiling((double)totalProduct / pageSize);

            var products = query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPage = totalPage;
            ViewBag.CategoryId = categoryId;
            ViewBag.Keyword = keyword;

            var customerId = HttpContext.Session.GetInt32("CustomerId");
            if (customerId != null)
            {
                ViewBag.WishlistProductIds = _context.WishlistItems
                    .Where(w => w.CustomerId == customerId.Value)
                    .Select(w => w.ProductId)
                    .ToList();
            }
            else
            {
                ViewBag.WishlistProductIds = new List<int>();
            }

            return View(products);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}