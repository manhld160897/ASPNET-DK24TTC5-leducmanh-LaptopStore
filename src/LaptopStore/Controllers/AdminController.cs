using Microsoft.AspNetCore.Mvc;

namespace LaptopStore.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            var role = HttpContext.Session.GetString("Role");
            if (role != "Admin")
            {
                return RedirectToAction("Login", "Account");
            }

            return View();
        }
    }
}