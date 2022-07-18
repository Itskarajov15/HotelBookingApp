using System.ComponentModel.DataAnnotations;

using static HotelBooking.Infrastructure.Data.DataConstants;

namespace HotelBooking.Infrastructure.Data.Models
{
    public class Client
    {
        public Client()
        {
            this.Reservations = new List<Reservation>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(FirstNameMaxLength)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(LastNameMaxLength)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(EmailMaxLength)]
        public string Email { get; set; }

        [Required]
        [StringLength(PhoneNumberMaxLength)]
        public string PhoneNumber { get; set; }

        public ICollection<Reservation> Reservations { get; set; }
    }
}