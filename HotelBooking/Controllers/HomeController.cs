using HotelBooking.Core.Contracts;
using HotelBooking.Core.Models.Rooms;
using HotelBooking.Core.Services;
using HotelBooking.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HotelBooking.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IHotelService hotelService;
        private readonly IService service;
        private readonly IReservationService reservationService;

        public HomeController(IHotelService hotelService,
            IService service,
            IReservationService reservationService)
        {
            this.hotelService = hotelService;
            this.service = service;
            this.reservationService = reservationService;
        }

        public IActionResult Index() => this.View(new FilterRoomsViewModel
        {
            Cities = this.hotelService.GetCityNames()
        });

        [HttpPost]
        public IActionResult Index(FilterRoomsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View();
            }

            if (!service.ValidateDates(model.StartDate, model.EndDate))
            {
                ModelState.AddModelError(String.Empty, "Invalid dates");
                return this.View();
            }

            return Ok(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}