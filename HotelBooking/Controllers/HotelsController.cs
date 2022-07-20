using HotelBooking.Core.Contracts;
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

        public IActionResult Add()
        {
            return this.View();
        }
    }
}