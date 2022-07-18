using System.ComponentModel.DataAnnotations;

using static HotelBooking.Infrastructure.Data.DataConstants;

namespace HotelBooking.Infrastructure.Data.Models
{
    public class RoomType
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(RoomTypeMaxLength)]
        public string TypeName { get; set; }
    }
}