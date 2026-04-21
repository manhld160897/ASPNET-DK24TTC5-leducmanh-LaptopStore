using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LaptopStore.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Tên sản phẩm không được để trống")]
        [StringLength(200)]
        public string Name { get; set; }

        public string? ImageUrl { get; set; }

        public string? Description { get; set; }

        [Required(ErrorMessage = "Giá không được để trống")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public string? CPU { get; set; }

        public string? RAM { get; set; }

        public string? Storage { get; set; }

        public int StockQuantity { get; set; }

        public int BrandId { get; set; }
        public Brand? Brand { get; set; }

        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public List<OrderDetail>? OrderDetails { get; set; }
    }
}
