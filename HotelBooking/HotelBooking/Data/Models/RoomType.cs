namespace HotelBooking.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static DataConstants;

    public class RoomType
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(RoomTypeMaxLength)]
        public string TypeName { get; set; }
    }
}