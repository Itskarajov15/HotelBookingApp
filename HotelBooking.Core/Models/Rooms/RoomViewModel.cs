namespace HotelBooking.Core.Models.Rooms
{
    public class RoomViewModel
    {
        public int Id { get; set; }

        public string RoomTypeName { get; set; }

        public List<string> RoomImageUrls { get; set; }

        public string Description { get; set; }

        public string HotelName { get; set; }

        public decimal PriceForOneNight { get; set; }
    }
}