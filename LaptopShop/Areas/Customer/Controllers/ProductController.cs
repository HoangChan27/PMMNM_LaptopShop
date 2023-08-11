using LaptopShop.Data;
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
        
	}
}
