using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LaptopShop.Models.EF
{
	public class Product
	{
        [Key]
        public int ProductId { get; set; }
        public string? Name { get; set; }
        public string? Brand { get; set; }
        [Display(Name = "OS")]
        public string? OperatingSystem { get; set; }
        public string? Monitor { get; set; }
        public string? CPU { get; set; }
        public string? RAM { get; set; }
        public string? Storage { get; set; }
        public string? Graphics { get; set; }
        public double? Weight { get; set; }
        public string? Color { get; set; }
        public string? Battery { get; set; }
        public byte? Warranty { get; set; } // số tháng bảo hành
        public string? Description { get; set; }
        public string? Image { get; set; }
        public decimal Price { get; set; }
        public int? Quantity { get; set; } // số lượng còn trong kho
        [Display(Name = "Category Name")]
        public int? CategoryId { get; set; }

        public virtual Category? Category { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        public Product()
        {
            this.CartItems = new HashSet<CartItem>();
            this.OrderDetails = new HashSet<OrderDetail>();
        }
    }
}
