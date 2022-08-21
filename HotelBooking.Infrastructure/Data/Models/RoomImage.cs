using System.ComponentModel.DataAnnotations;

namespace HotelBooking.Infrastructure.Data.Models
{
    public class RoomImage
    {
        public int Id { get; set; }

        [Required]
        public string Url { get; set; }

        public int RoomId { get; set; }

        public Room Room { get; set; }
    }
}