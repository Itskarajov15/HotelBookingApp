using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace HotelBooking.Infrastructure.Data.Identity
{
    public class User : IdentityUser
    {
        [MaxLength(60)]
        public string FullName { get; set; }
    }
}