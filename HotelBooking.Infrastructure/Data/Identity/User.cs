using HotelBooking.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace HotelBooking.Infrastructure.Data.Identity
{
    public class User : IdentityUser
    {
        public User()
        {
            this.Reservations = new List<Reservation>();
        }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        public ICollection<Reservation> Reservations { get; set; }
    }
}