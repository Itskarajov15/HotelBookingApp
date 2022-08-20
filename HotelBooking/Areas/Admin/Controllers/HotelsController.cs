using HotelBooking.Core.Contracts;
using HotelBooking.Core.Models.Hotels;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.Areas.Admin.Controllers
{
    public class HotelsController : BaseController
    {
        private readonly IHotelService hotelService;

        public HotelsController(IHotelService hotelService)
        {
            this.hotelService = hotelService;
        }

        public IActionResult ManageHotels()
        {
            return Ok();
        }

        public IActionResult Add() => this.View(new AddHotelViewModel
        {
            Cities = this.hotelService.GetCityNames()
        });

        [HttpPost]
        public IActionResult Add(AddHotelViewModel hotel)
        {
            if (!this.hotelService.IsCityValid(hotel.CityId))
            {
                ModelState.AddModelError(nameof(hotel.CityId), "City does not exist");
            }

            if (!ModelState.IsValid)
            {
                hotel.Cities = this.hotelService.GetCityNames();
                return this.View(hotel);
            }

            var isAdded = this.hotelService.AddHotel(hotel);

            if (!isAdded)
            {
                return this.View(hotel);
            }

            return RedirectToAction("ManageHotels", "Hotels");
        }
    }
}