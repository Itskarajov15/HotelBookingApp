namespace HotelBooking.Core.Models.Hotels
{
    public class HotelViewModel
    {
        public string HotelName { get; set; }

        public string Description { get; set; }

        public string PrimaryImageUrl { get; set; }

        public string CityName { get; set; }

        public string CountryName { get; set; }

        public List<string> HotelImages { get; set; }
    }
}