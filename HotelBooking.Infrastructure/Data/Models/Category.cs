using System.ComponentModel.DataAnnotations;

using static HotelBooking.Infrastructure.Data.DataConstants;

namespace HotelBooking.Infrastructure.Data.Models
{
    public class Category
    {
        public Category()
        {
            this.Hotels = new List<Hotel>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(CategoryNameMaxLength)]
        public string CategoryName { get; set; }

        public ICollection<Hotel> Hotels { get; set; }
    }
}