namespace HotelBooking.Core.Models.Users
{
    public class UserReservationViewModel
    {
        public string HotelName { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public string GuestName { get; set; }

        public string RoomType { get; set; }

        public int Id { get; set; }

        public decimal TotalPrice { get; set; }
    }
}