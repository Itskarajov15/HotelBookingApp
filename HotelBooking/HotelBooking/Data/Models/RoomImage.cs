namespace HotelBooking.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class RoomImage
    {
        public int Id { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public int RoomId { get; set; }

        public Room Room { get; set; }
    }
}