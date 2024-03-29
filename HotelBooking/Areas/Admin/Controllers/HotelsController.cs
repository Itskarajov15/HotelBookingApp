﻿using HotelBooking.Core.Contracts;
using HotelBooking.Core.Models.Hotels;
using HotelBooking.Core.Models.Users;
using HotelBooking.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.Areas.Admin.Controllers
{
    public class HotelsController : BaseController
    {
        private readonly IHotelService hotelService;
        private readonly ICityService cityService;

        public HotelsController(IHotelService hotelService,
            ICityService cityService)
        {
            this.hotelService = hotelService;
            this.cityService = cityService;
        }

        public IActionResult ManageHotels(string searchString)
        {
            var hotels = this.hotelService.GetHotelsForManaging(searchString);

            return this.View(hotels);
        }

        [HttpPost]
        public JsonResult AutoComplete(string prefix)
        {
            var hotels = this.hotelService.GetHotelsForManaging()
                             .Where(u => u.Name.ToLower().StartsWith(prefix.ToLower()))
                             .Select(u => new
                             {
                                 label = u.Name,
                                 val = u.Name
                             })
                             .ToList();

            return Json(hotels);
        }

        public IActionResult Add() => this.View(new AddHotelViewModel
        {
            Cities = this.cityService.GetCityNames()
        });

        [HttpPost]
        public async Task<IActionResult> Add(AddHotelViewModel hotel)
        {
            if (!this.cityService.IsCityValid(hotel.CityId))
            {
                ModelState.AddModelError(nameof(hotel.CityId), "City does not exist");
            }

            if (!ModelState.IsValid)
            {
                hotel.Cities = this.cityService.GetCityNames();
                return this.View(hotel);
            }

            var isAdded = await this.hotelService.AddHotel(hotel);

            if (!isAdded)
            {
                return this.View(hotel);
            }

            return RedirectToAction("ManageHotels", "Hotels");
        }
    }
}