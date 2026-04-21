using System.ComponentModel.DataAnnotations;

namespace LaptopStore.Models
{
    public class WishlistItem
    {
        [Key]
        public int WishlistItemId { get; set; }

        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }

        public int ProductId { get; set; }
        public Product? Product { get; set; }
    }
}