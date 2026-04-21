using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LaptopStore.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.Now;

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }

        [StringLength(50)]
        public string Status { get; set; } = "Pending";
        // Pending, Confirmed, Shipping, Completed, Cancelled

        [StringLength(255)]
        public string ShippingAddress { get; set; }

        [StringLength(100)]
        public string ReceiverName { get; set; }

        [StringLength(20)]
        public string ReceiverPhone { get; set; }

        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }

        public List<OrderDetail>? OrderDetails { get; set; }
        [NotMapped]
        public string StatusVN
        {
            get
            {
                return Status switch
                {
                    "Pending" => "Chờ xác nhận",
                    "Confirmed" => "Đã xác nhận",
                    "Shipping" => "Đang giao hàng",
                    "Completed" => "Hoàn thành",
                    "Cancelled" => "Đã hủy",
                    _ => Status
                };
            }
        }
    }

}