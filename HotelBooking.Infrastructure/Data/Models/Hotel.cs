using System.ComponentModel.DataAnnotations;

using static HotelBooking.Infrastructure.Data.DataConstants;

namespace HotelBooking.Infrastructure.Data.Models
{
    public class Hotel
    {
        public Hotel()
        {
            this.HotelImages = new List<HotelImage>();
            this.Rooms = new List<Room>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(HotelNameMaxLength)]
        public string HotelName { get; set; }

        [Required]
        public string Description { get; set; }

        public string PrimaryImageUrl { get; set; }

        public int CityId { get; set; }

        public City City { get; set; }

        public ICollection<HotelImage> HotelImages { get; set; }

        public ICollection<Room> Rooms { get; set; }
    }
}