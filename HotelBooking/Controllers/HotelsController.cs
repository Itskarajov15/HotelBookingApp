using HotelBooking.Core.Contracts;
using HotelBooking.Core.Models.Hotels;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.Controllers
{
    public class HotelsController : Controller
    {
        private readonly IHotelService hotelService;
        private readonly IRoomService roomService;

        public HotelsController(IHotelService service, IRoomService roomService)
        {
            this.hotelService = service;
            this.roomService = roomService;
        }

        public IActionResult All()
        {
            var hotels = hotelService.GetAllHotels();

            return this.View(hotels);
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

            return RedirectToAction("All", "Hotels");
        }

        public IActionResult Details(int id)
        {
            var hotel = hotelService.GetHotel(id);

            if (hotel == null)
            {
                return RedirectToAction("All", "Hotels");
            }

            this.ViewBag.Rooms = this.roomService.GetAllRooms();

            return this.View(hotel);
        }
    }
}