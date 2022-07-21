using HotelBooking.Core.Contracts;
using HotelBooking.Core.Models.Hotels;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.Controllers
{
    public class HotelsController : Controller
    {
        private readonly IHotelService service;

        public HotelsController(IHotelService service)
        {
            this.service = service;
        }

        public IActionResult All()
        {
            var hotels = service.GetAllHotels();

            return this.View(hotels);
        }

        public IActionResult Add() => this.View(new AddHotelViewModel
        {
            Cities = this.service.GetCityNames()
        });

        [HttpPost]
        public IActionResult Add(AddHotelViewModel hotel)
        {
            if (!this.service.IsCityValid(hotel.CityId))
            {
                ModelState.AddModelError(nameof(hotel.CityId), "City does not exist");
            }

            if (!ModelState.IsValid)
            {
                hotel.Cities = this.service.GetCityNames();
                return this.View(hotel);
            }

            var isAdded = this.service.AddHotel(hotel);

            if (!isAdded)
            {
                return this.View(hotel);
            }

            return RedirectToAction("All", "Hotels");
        }
    }
}