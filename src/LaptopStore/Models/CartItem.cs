using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LaptopStore.Models
{
    public class CartItem
    {
        [Key]
        public int CartItemId { get; set; }

        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }

        public int ProductId { get; set; }
        public Product? Product { get; set; }

        public int Quantity { get; set; }

        [NotMapped]
        public decimal TotalPrice => (Product != null ? Product.Price : 0) * Quantity;
    }
}