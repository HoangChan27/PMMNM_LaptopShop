using LaptopShop.Data;
using LaptopShop.Extension;
using LaptopShop.Models.EF;
using LaptopShop.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace LaptopShop.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class ProductController : Controller
    {
        private readonly LaptopDbContext _context;

        public ProductController(LaptopDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            List<ProductDto> listDto = _context.Products.Include(x => x.Category).Select(x => new ProductDto
			{
				ProductId = x.ProductId,
				Name = x.Name,
				CategoryName = x.Category.CategoryName,
				Price = x.Price,
				Image = x.Image
			}).ToList();
			if(listDto == null)
			{
				return BadRequest();
			}
			ViewBag.list = listDto;
			return View();
		}

        public ActionResult Detail(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var product = _context.Products.Include(c => c.Category).FirstOrDefault(c => c.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }


        //POST product detail acation method
        [HttpPost]
        [ActionName("Detail")]
        public ActionResult ProductDetail(int? id)
        {
            List<Product> products = new List<Product>();
            if (id == null)
            {
                return NotFound();
            }

            var product = _context.Products.Include(c => c.Category).FirstOrDefault(c => c.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            products = HttpContext.Session.GetJson<List<Product>>("products");
            if (products == null)
            {
                products = new List<Product>();
            }
            products.Add(product);
            HttpContext.Session.SetJson("products", products);
            return RedirectToAction(nameof(Index));
        }
    }
}
