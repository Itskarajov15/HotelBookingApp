namespace HotelBooking.Core.Models.Hotels
{
    public class HotelViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string PrimaryImageUrl { get; set; }

        public string CityName { get; set; }

        public List<string> HotelImages { get; set; }
    }
}