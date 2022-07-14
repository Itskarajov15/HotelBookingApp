﻿namespace HotelBooking.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class HotelImage
    {
        public int Id { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public int HotelId { get; set; }

        public Hotel Hotel { get; set; }
    }
}