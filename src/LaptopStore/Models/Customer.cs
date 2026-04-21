using System.ComponentModel.DataAnnotations;

namespace LaptopStore.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Họ tên không được để trống")]
        [StringLength(100)]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email không được để trống")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        public string Password { get; set; }

        [StringLength(255)]
        public string? Address { get; set; }
        public string Role { get; set; } = "Customer";

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public List<Order>? Orders { get; set; }
    }
}