using AspNetCoreHero.ToastNotification.Notyf;
using HotelBooking.Core.Contracts;
using HotelBooking.Core.Models.Hotels;
using HotelBooking.Core.Models.Users;
using HotelBooking.Core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HotelBooking.Areas.Admin.Controllers
{
    public class HotelsController : BaseController
    {
        private readonly IHotelService hotelService;

        public HotelsController(IHotelService hotelService)
        {
            this.hotelService = hotelService;
        }

        public IActionResult ManageHotels(string searchString)
        {
            IEnumerable<HotelListViewModel> hotels;

            if (searchString == null)
            {
                hotels = hotelService.GetAllHotelsForManage();
            }
            else
            {
                hotels = hotelService.GetAllHotelsForManage(searchString);
            }

            return View(hotels);
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

        [HttpPost]
        public JsonResult AutoComplete(string prefix)
        {
            var hotels = this.hotelService.GetAllHotels()
                             .Where(h => h.HotelName.ToLower().StartsWith(prefix.ToLower()))
                             .Select(h => new
                             {
                                 label = h.HotelName,
                                 val = h.HotelName
                             })
                             .ToList();
            return Json(hotels);
        }

        public IActionResult Edit(int id)
        {
            var model = hotelService.GetHotelForEdit(id);

            ViewBag.Cities = hotelService.GetCityNames()
                                   .Select(c => new SelectListItem()
                                   {
                                       Text = c.Name,
                                       Value = c.Name,
                                       Selected = c.Id == model.CityId
                                   })
                                   .ToList();

            return View(model);
        }

        //[HttpPost]
        //public async Task<IActionResult> Edit(string id, UserEditViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return this.View(model);
        //    }

        //    if (await userService.UpdateUser(model))
        //    {
        //        notyfService.Success("User is edited successfully!");
        //    }
        //    else
        //    {
        //        notyfService.Error("There is a problem with the edit");
        //    }

        //    return View(model);
        //}
    }
}