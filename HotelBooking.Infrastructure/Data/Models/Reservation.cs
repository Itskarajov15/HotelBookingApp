using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBooking.Infrastructure.Data.Models
{
    public class Reservation
    {
        public int Id { get; set; }

        public int ClientId { get; set; }

        public Client Client { get; set; }

        public int RoomId { get; set; }

        public Room Room { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; }
    }
}