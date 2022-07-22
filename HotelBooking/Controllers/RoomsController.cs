using HotelBooking.Core.Contracts;
using HotelBooking.Core.Models.Rooms;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.Controllers
{
    public class RoomsController : Controller
    {
        private readonly IRoomService service;

        public RoomsController(IRoomService service)
        {
            this.service = service;
        }

        public IActionResult Add(int hotelId) => this.View(new AddRoomViewModel
        {
            HotelId = hotelId,
            RoomTypes = this.service.GetRoomTypes()
        });
    }
}