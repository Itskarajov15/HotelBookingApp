using HotelBooking.Core.Contracts;
using HotelBooking.Core.Models.Rooms;
using HotelBooking.Infrastructure.Data.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.Controllers
{
    public class RoomsController : BaseController
    {
        private readonly IRoomService roomService;

        public RoomsController(IRoomService roomService)
        {
            this.roomService = roomService;
        }

        public IActionResult Add() => this.View(new AddRoomViewModel
        {
            RoomTypes = this.roomService.GetRoomTypes()
        });

        [HttpPost]
        public async Task<IActionResult> Add(int id, AddRoomViewModel roomModel)
        {
            if (!ModelState.IsValid)
            {
                roomModel.RoomTypes = this.roomService.GetRoomTypes();
                return this.View(roomModel);
            }

            roomModel.HotelId = id;

            var isAdded = await this.roomService.AddRoom(roomModel);

            if (!isAdded)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong");
                roomModel.RoomTypes = this.roomService.GetRoomTypes();
                return this.View(roomModel);
            }

            return RedirectToAction("Details", "Hotels", new { id = id });
        }

        public IActionResult Details(int id)
        {
            var room = this.roomService.GetRoom(id);

            if (room == null)
            {
                RedirectToAction("All", "Hotels");
            }

            return this.View(room);
        }
    }
}