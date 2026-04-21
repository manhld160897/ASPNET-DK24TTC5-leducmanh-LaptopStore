using LaptopStore.Models;
using LaptopStore.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LaptopStore.Controllers
{
    public class AdminProductController : Controller
    {
        private readonly DataContext _context;

        public AdminProductController(DataContext context)
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

            var products = _context.Products
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .ToList();

            return View(products);
        }

        public IActionResult Create()
        {
            if (!CheckAdmin()) return RedirectToAction("Login", "Account");

            ViewBag.Brands = new SelectList(_context.Brands.ToList(), "BrandId", "Name");
            ViewBag.Categories = new SelectList(_context.Categories.ToList(), "CategoryId", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (!CheckAdmin()) return RedirectToAction("Login", "Account");

            if (ModelState.IsValid)
            {
                _context.Products.Add(product);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Brands = new SelectList(_context.Brands.ToList(), "BrandId", "Name", product.BrandId);
            ViewBag.Categories = new SelectList(_context.Categories.ToList(), "CategoryId", "Name", product.CategoryId);
            return View(product);
        }

        public IActionResult Edit(int id)
        {
            if (!CheckAdmin()) return RedirectToAction("Login", "Account");

            var product = _context.Products.Find(id);
            if (product == null) return NotFound();

            ViewBag.Brands = new SelectList(_context.Brands.ToList(), "BrandId", "Name", product.BrandId);
            ViewBag.Categories = new SelectList(_context.Categories.ToList(), "CategoryId", "Name", product.CategoryId);
            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (!CheckAdmin()) return RedirectToAction("Login", "Account");

            if (ModelState.IsValid)
            {
                _context.Products.Update(product);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Brands = new SelectList(_context.Brands.ToList(), "BrandId", "Name", product.BrandId);
            ViewBag.Categories = new SelectList(_context.Categories.ToList(), "CategoryId", "Name", product.CategoryId);
            return View(product);
        }

        public IActionResult Delete(int id)
        {
            if (!CheckAdmin()) return RedirectToAction("Login", "Account");

            var product = _context.Products
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .FirstOrDefault(p => p.ProductId == id);

            if (product == null) return NotFound();

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            if (!CheckAdmin()) return RedirectToAction("Login", "Account");

            var product = _context.Products.Find(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}