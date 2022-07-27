using System.ComponentModel.DataAnnotations;

namespace HotelBooking.Core.Models.Rooms
{
    public class ReserveRoomViewModel
    {
        public int RoomId { get; set; }

        public int ClientId { get; set; }

        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        [Range(1, 4, ErrorMessage = "The {0} must be between {1} and {2}")]
        [Display(Name = "Count of People")]
        public int CountOfPeople { get; set; }

        public decimal TotalPrice { get; set; }
    }
}