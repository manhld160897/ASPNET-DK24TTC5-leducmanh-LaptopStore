using System.ComponentModel.DataAnnotations;

namespace LaptopStore.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên laptop không được để trống")]
        [StringLength(200)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Giá không được để trống")]
        [Range(0, 999999999)]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Số lượng không được để trống")]
        [Range(0, 1000)]
        public int Quantity { get; set; }

        [StringLength(100)]
        public string Brand { get; set; }

        [StringLength(100)]
        public string Cpu { get; set; }

        [StringLength(100)]
        public string Ram { get; set; }

        [StringLength(100)]
        public string Storage { get; set; }

        [StringLength(500)]
        public string ImageUrl { get; set; }

        public string Description { get; set; }
    }
}