using System.ComponentModel.DataAnnotations;

namespace HotelBooking.Core.Models.Rooms
{
    public class ReserveRoomViewModel
    {
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }
    }
}