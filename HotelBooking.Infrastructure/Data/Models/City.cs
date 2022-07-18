using System.ComponentModel.DataAnnotations;

using static HotelBooking.Infrastructure.Data.DataConstants;

namespace HotelBooking.Infrastructure.Data.Models
{
    public class City
    {
        public City()
        {
            this.Hotels = new List<Hotel>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(CityNameMaxLength)]
        public string CityName { get; set; }

        public int CountryId { get; set; }

        public Country Country { get; set; }

        public ICollection<Hotel> Hotels { get; set; }
    }
}