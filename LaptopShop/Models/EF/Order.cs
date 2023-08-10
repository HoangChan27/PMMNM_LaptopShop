using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LaptopShop.Models.EF
{
	public class Order
	{
        [Key]
        public int OrderId { get; set; }
        public DateTime? OrderDate { get; set; }
        public bool? Paid { get; set; }
        public string? Note { get; set; }
        public string? Status { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        public Order()
        {
            this.OrderDetails = new HashSet<OrderDetail>();
        }
    }
}
