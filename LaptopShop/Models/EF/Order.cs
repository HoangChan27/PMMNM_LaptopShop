using System.ComponentModel.DataAnnotations;

namespace LaptopShop.Models.EF
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public DateTime? OrderDate { get; set; }
        public decimal GrandTotal { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
