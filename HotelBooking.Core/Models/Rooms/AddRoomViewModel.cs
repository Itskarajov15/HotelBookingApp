using Microsoft.AspNetCore.Http;
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

        [Required(ErrorMessage = "Please select files")]
        [Display(Name = "Room Images")]
        public List<IFormFile> Images { get; set; }

        public int RoomTypeId { get; set; }

        public int HotelId { get; set; }

        public IEnumerable<RoomTypeViewModel>? RoomTypes { get; set; }
    }
}