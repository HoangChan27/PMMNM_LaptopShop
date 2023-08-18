using LaptopShop.Data;
using LaptopShop.Models;
using LaptopShop.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace LaptopShop.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly LaptopDbContext _context;

        public HomeController(ILogger<HomeController> logger, LaptopDbContext context)
        {
            _logger = logger;
            _context = context;
        }



        public IActionResult Index()
        {
            var listProduct = _context.Products.Include(x => x.Category).Select(x => new ProductDto
            {
                ProductId = x.ProductId,
                Name = x.Name,
                CategoryName = x.Category.CategoryName,
                Price = x.Price,
                Image = x.Image
            }).ToList();
            return View(listProduct);
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
