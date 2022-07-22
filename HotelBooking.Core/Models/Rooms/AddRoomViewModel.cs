using System.ComponentModel.DataAnnotations;

namespace HotelBooking.Core.Models.Rooms
{
    public class AddRoomViewModel
    {
        [Required]
        [StringLength(100, MinimumLength = 10, ErrorMessage = "{0} must be between {2} and {1} characters")]
        public string Description { get; set; }

        [Range(0, 1000, ErrorMessage = "{0} must be between {1} and {2} leva")]
        public decimal PriceForOneNight { get; set; }

        [Display(Name = "First Room Image")]
        [Required]
        [Url]
        public string FirstRoomImageUrl { get; set; }

        [Display(Name = "Second Room Image")]
        [Required]
        [Url]
        public string SecondRoomImageUrl { get; set; }

        [Display(Name = "Third Room Image")]
        [Required]
        [Url]
        public string ThirdRoomImageUrl { get; set; }

        public int HotelId { get; set; }

        public int RoomTypeId { get; set; }

        public IEnumerable<RoomTypeViewModel>? RoomTypes { get; set; }
    }
}