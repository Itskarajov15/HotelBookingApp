using System.ComponentModel.DataAnnotations;

namespace HotelBooking.Core.Models.Hotels
{
    public class AddHotelViewModel
    {
        [Display(Name = "Hotel Name")]
        public string HotelName { get; set; }

        public string Description { get; set; }

        [Display(Name = "Primary Hotel Image")]
        public string PrimaryImageUrl { get; set; }

        [Display(Name = "First Additional Hotel Image")]
        public string FirstAdditionalImageUrl { get; set; }

        [Display(Name = "Second Additional Hotel Image")]
        public string SecondAdditionalImageUrl { get; set; }

        public int CityId { get; set; }
    }
}