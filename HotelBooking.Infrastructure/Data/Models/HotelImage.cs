using System.ComponentModel.DataAnnotations;

namespace HotelBooking.Infrastructure.Data.Models
{
    public class HotelImage
    {
        public int Id { get; set; }

        [Required]
        public string Url { get; set; }

        public int HotelId { get; set; }

        public Hotel Hotel { get; set; }
    }
}