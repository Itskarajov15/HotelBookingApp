using System.ComponentModel.DataAnnotations;

using static HotelBooking.Infrastructure.Data.DataConstants;

namespace HotelBooking.Infrastructure.Data.Models
{
    public class RoomType
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(RoomTypeMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(RoomTypeDescriptionMaxLength)]
        public string Description { get; set; }

        public int CountOfPeople { get; set; }
    }
}