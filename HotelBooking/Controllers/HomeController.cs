using HotelBooking.Core.Contracts;
using HotelBooking.Core.Models.Rooms;
using HotelBooking.Core.Services;
using HotelBooking.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace HotelBooking.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IService service;
        private readonly ICityService cityService;
        private readonly IReservationService reservationService;

        public HomeController(IService service,
            ICityService cityService,
            IReservationService reservationService)
        {
            this.service = service;
            this.cityService = cityService;
            this.reservationService = reservationService;
        }

        public IActionResult Index() => this.View(new FilterRoomsViewModel
        {
            Cities = this.cityService.GetCityNames()
        });

        //[HttpPost]
        //public IActionResult Index(FilterRoomsViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return this.View();
        //    }

        //    if (!service.ValidateDates(model.StartDate, model.EndDate))
        //    {
        //        ModelState.AddModelError(String.Empty, "Invalid dates");
        //        return this.View();
        //    }

        //    var result = this.reservationService.GetFreeRooms(model);

        //    return Ok(result);
        //}

        [HttpPost]
        public IActionResult GetFreeRooms([FromBody]FilterRoomsViewModel model)
        {
            if (!ModelState.IsValid || !service.ValidateDates(model.StartDate, model.EndDate))
            {
                if (!service.ValidateDates(model.StartDate, model.EndDate))
                {
                    ModelState.AddModelError(String.Empty, "Invalid dates");
                }

                var errors = ModelState.Values.SelectMany(x => x.Errors);

                return Json(errors);
            }

            var result = this.reservationService.GetFreeRooms(model);

            return Json(result);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}