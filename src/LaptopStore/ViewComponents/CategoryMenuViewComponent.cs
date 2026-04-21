using LaptopStore.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LaptopStore.ViewComponents
{
    public class CategoryMenuViewComponent : ViewComponent
    {
        private readonly DataContext _context;

        public CategoryMenuViewComponent(DataContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _context.Categories.ToListAsync();
            return View(categories);
        }
    }
}