using System.ComponentModel.DataAnnotations;

using static HotelBooking.Infrastructure.Data.DataConstants;

namespace HotelBooking.Infrastructure.Data.Models
{
    public class Country
    {
        public Country()
        {
            this.Cities = new List<City>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(ContryNameMaxLength)]
        public string Name { get; set; }

        public ICollection<City> Cities { get; set; }
    }
}