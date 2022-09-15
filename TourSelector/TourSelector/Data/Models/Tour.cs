using System.ComponentModel.DataAnnotations;

namespace TourSelector.Data.Models
{
    public class Tour
    {
        [Required]
        public Customer Customer { get; set; } = new Customer();

        [Required]
        public string TourName { get; set; } = "";
    }
}
