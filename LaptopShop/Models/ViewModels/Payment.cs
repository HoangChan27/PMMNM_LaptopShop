using LaptopShop.Models.EF;
using System.ComponentModel.DataAnnotations;

namespace LaptopShop.Models.ViewModels
{
	public class Payment
	{
		
		public decimal GrandTotal { get; set; }
		public string FullName { get; set; }
		public string Email { get; set; }
		public string Phone { get; set; }
		public string Address { get; set; }
	}
}
