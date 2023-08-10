using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LaptopShop.Models.EF
{
	public class OrderDetail
	{
        [Key]
        public int OrderDetailId { get; set; }
        public int? Quantity { get; set; } // số lượng sản phẩm trong đơn hàng
        public decimal? Price { get; set; }
        public decimal? Total { get { return Quantity * Price; } }
        public DateTime? RecievedDate { get; set; } // ngày giao hàng thành công
        public string? Status { get; set; } 
        public int OrderId { get; set; }
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product? Product { get; set; }
        [ForeignKey("OrderId")]
        public virtual Order? Order { get; set; }
    }

}
