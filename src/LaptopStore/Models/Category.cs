using System.ComponentModel.DataAnnotations;

namespace LaptopStore.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Tên danh mục không được để trống")]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(255)]
        public string? Description { get; set; }

        public List<Product>? Products { get; set; }
    }
}