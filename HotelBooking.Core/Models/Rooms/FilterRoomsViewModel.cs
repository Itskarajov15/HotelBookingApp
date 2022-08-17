using HotelBooking.Core.Models.Hotels;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace HotelBooking.Core.Models.Rooms
{
    public class FilterRoomsViewModel
    {
        [Display(Name = "City")]
        public int CityId { get; set; }

        public IEnumerable<HotelCityViewModel>? Cities { get; set; }

        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Count of People")]
        [Range(1, 4, ErrorMessage = "Count of People must be between {1} and {2}")]
        public int CountOfPeople { get; set; }
    }
}