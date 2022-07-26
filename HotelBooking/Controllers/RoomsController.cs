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

        public IActionResult Add() => this.View(new AddRoomViewModel
        {
            RoomTypes = this.service.GetRoomTypes()
        });

        [HttpPost]
        public IActionResult Add(int id, AddRoomViewModel roomModel)
        {
            if (!ModelState.IsValid)
            {
                roomModel.RoomTypes = this.service.GetRoomTypes();
                return this.View(roomModel);
            }

            roomModel.HotelId = id;

            var isAdded = this.service.AddRoom(roomModel);

            if (!isAdded)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong");
                roomModel.RoomTypes = this.service.GetRoomTypes();
                return this.View(roomModel);
            }

            return RedirectToAction("Details", "Hotels", new { id = id });
        }

        public IActionResult Details(int id)
        {
            var room = this.service.GetRoom(id);

            if (room == null)
            {
                RedirectToAction("All", "Hotels");
            }

            return this.View(room);
        }
    }
}