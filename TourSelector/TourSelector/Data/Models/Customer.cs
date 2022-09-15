using System.ComponentModel.DataAnnotations;

namespace TourSelector.Data.Models
{
    public class Customer
    {
        [Required]
        [StringLength(100, ErrorMessage = "Name is too long.")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(320, ErrorMessage = "Email is not valid.")]
        public string Email { get; set; } = string.Empty;
    }
}
