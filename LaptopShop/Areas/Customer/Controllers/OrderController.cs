using LaptopShop.Data;
using LaptopShop.Extension;
using LaptopShop.Models.EF;
using LaptopShop.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace LaptopShop.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class OrderController : Controller
    {
		private readonly LaptopDbContext _context;

		public OrderController(LaptopDbContext context)
        {
				_context = context;
        }
        public IActionResult Checkout()
        {
            var cart = HttpContext.Session.GetJson<List<CartItem>>("Cart");
			CartViewModel cartViewModel = new CartViewModel();
			{
				cartViewModel.CartItems = cart;
				cartViewModel.GrandTotal = cart.Sum(x => x.Quantity * x.Price);
			};
			Payment pm = null;
			var checkoutVM = new CheckoutViewModel
			{
				Cart = cartViewModel,
				Payment = pm
			};
			if (!string.IsNullOrEmpty(User.Identity.Name))
            {
				
				return View(checkoutVM);

			}
			return RedirectToAction("Login","UserAuthentication",new {area ="Identity"});
		}
		[HttpPost]
        public IActionResult Order(Payment payment)
        {
			

			List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart");
			CartViewModel cartViewModel = new CartViewModel();
			{
				cartViewModel.CartItems = cart;
				cartViewModel.GrandTotal = cart.Sum(x => x.Quantity * x.Price);
			};
			payment.GrandTotal = cartViewModel.GrandTotal;
			
			if (cart == null)
			{
				return RedirectToAction("Index", "Home");
			}
			if (ModelState.IsValid)
			{
				var checkoutVM = new CheckoutViewModel
				{
					Cart = cartViewModel,
					Payment = payment
				};
				Order order = new Order()
				{
					OrderDate = DateTime.Now,
					GrandTotal = payment.GrandTotal,
					FullName = payment.FullName,
					Email = payment.Email,
					Address = payment.Address,
					Phone = payment.Phone

				};
				
				_context.Orders.Add(order);
				_context.SaveChanges();
				foreach(var item in cart)
				{
					OrderDetail orderDetail = new OrderDetail();
					orderDetail.OrderId = order.OrderId;
					orderDetail.ProductId = item.ProductId;
					orderDetail.Quantity = item.Quantity;
					orderDetail.Price = item.Price;					
					_context.Add(orderDetail);
				}
				_context.SaveChanges();

				HttpContext.Session.Remove("Cart");
				return View(checkoutVM);
			}
			return RedirectToAction(nameof(Checkout));
		}
		public ActionResult ListOrder()
		{
			List<Order> orders = HttpContext.Session.GetJson<List<Order>>("Orders");
			return View(orders);
		}
       
        
    }
}
