namespace HotelBooking.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using static DataConstants;

    public class Country
    {
        public Country()
        {
            this.Cities = new List<City>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(ContryNameMaxLength)]
        public string CountryName { get; set; }

        public ICollection<City> Cities { get; set; }
    }
}