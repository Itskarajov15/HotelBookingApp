using System.ComponentModel.DataAnnotations;

using static HotelBooking.Infrastructure.Data.DataConstants;

namespace HotelBooking.Core.Models.Hotels
{
    public class HotelEditViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Hotel Name")]
        [Required]
        [StringLength(HotelNameMaxLength, MinimumLength = HotelNameMinLength, ErrorMessage = "{0} must be between {2} and {1} characters")]
        public string HotelName { get; set; }

        [Required]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength, ErrorMessage = "{0} must be between {2} and {1} characters")]
        public string Description { get; set; }

        [Display(Name = "Primary Hotel Image")]
        [Required]
        [Url]
        public string PrimaryImageUrl { get; set; }

        [Display(Name = "First Additional Hotel Image")]
        [Required]
        [Url]
        public string FirstAdditionalImageUrl { get; set; }

        [Display(Name = "Second Additional Hotel Image")]
        [Required]
        [Url]
        public string SecondAdditionalImageUrl { get; set; }

        [Display(Name = "City")]
        public int CityId { get; set; }

        public IEnumerable<HotelCityViewModel>? Cities { get; set; }
    }
}