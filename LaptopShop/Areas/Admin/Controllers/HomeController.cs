using Microsoft.AspNetCore.Mvc;

namespace LaptopShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

		public IActionResult Account()
        {
            return View();
        }

        public IActionResult Settings()
        {
            return View();
        }
    }
}
