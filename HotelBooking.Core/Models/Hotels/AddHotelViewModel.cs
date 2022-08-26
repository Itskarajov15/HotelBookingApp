﻿using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

using static HotelBooking.Infrastructure.Data.DataConstants;

namespace HotelBooking.Core.Models.Hotels
{
    public class AddHotelViewModel
    {
        [Display(Name = "Hotel Name")]
        [Required]
        [StringLength(HotelNameMaxLength, MinimumLength = HotelNameMinLength, ErrorMessage = "{0} must be between {2} and {1} characters")]
        public string Name { get; set; }

        [Required]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength, ErrorMessage = "{0} must be between {2} and {1} characters")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please select files")]
        public List<IFormFile> Files { get; set; }

        [Display(Name = "City")]
        public int CityId { get; set; }

        public IEnumerable<HotelCityViewModel>? Cities { get; set; }
    }
}