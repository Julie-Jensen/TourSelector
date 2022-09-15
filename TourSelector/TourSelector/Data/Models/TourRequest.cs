using System.ComponentModel.DataAnnotations;

namespace TourSelector.Data.Models
{
    public enum RequestType { Book, Cancel }

    public class TourRequest
    {
        [Required]
        public Tour Tour { get; set; } = new Tour();

        [Required]
        public RequestType RequestType { get; set; }
    }
}
