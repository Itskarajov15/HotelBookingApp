using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBooking.Infrastructure.Data.Models
{
    public class Room
    {
        public Room()
        {
            this.RoomImages = new List<RoomImage>();
        }

        public int Id { get; set; }

        [Required]
        public string Description { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal PriceForOneNight { get; set; }

        public bool IsReserved { get; set; }

        public int HotelId { get; set; }

        public Hotel Hotel { get; set; }

        public int RoomTypeId { get; set; }

        public RoomType RoomType { get; set; }

        public ICollection<RoomImage> RoomImages { get; set; }
    }
}