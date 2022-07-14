namespace HotelBooking.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static DataConstants;

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