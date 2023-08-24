using LaptopShop.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LaptopShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderController : Controller
    {
        private readonly LaptopDbContext _context;

        public OrderController(LaptopDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Order
        public async Task<IActionResult> Index()
        {
            return _context.Orders != null ?
                        View(await _context.Orders.ToListAsync()) :
                        Problem("Entity set 'LaptopDbContext.Orders'  is null.");
        }

        // GET: Admin/Order/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        private bool OrderExists(int id)
        {
            return (_context.Orders?.Any(e => e.OrderId == id)).GetValueOrDefault();
        }
    }
}
