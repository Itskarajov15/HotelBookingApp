using HotelBooking.Core.Contracts;
using HotelBooking.Core.Models.Rooms;
using HotelBooking.Infrastructure.Data.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.Controllers
{
    [Authorize]
    public class RoomsController : Controller
    {
        private readonly IRoomService roomService;
        private readonly UserManager<User> userManager;
        private readonly IService service;

        public RoomsController(IRoomService roomService, UserManager<User> userManager, IService service)
        {
            this.roomService = roomService;
            this.userManager = userManager;
            this.service = service;
        }

        public IActionResult Add() => this.View(new AddRoomViewModel
        {
            RoomTypes = this.roomService.GetRoomTypes()
        });

        [HttpPost]
        public IActionResult Add(int id, AddRoomViewModel roomModel)
        {
            if (!ModelState.IsValid)
            {
                roomModel.RoomTypes = this.roomService.GetRoomTypes();
                return this.View(roomModel);
            }

            roomModel.HotelId = id;

            var isAdded = this.roomService.AddRoom(roomModel);

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

        public IActionResult Reserve()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Reserve(int id, ReserveRoomViewModel reserveRoom)
        {
            if (!ModelState.IsValid)
            {
                return this.View();
            }

            if (!service.ValidateDates(reserveRoom.StartDate, reserveRoom.EndDate))
            {
                ModelState.AddModelError(String.Empty, "Invalid dates");
                return this.View();
            }

            var isReserved = this.roomService.ReserveRoom(reserveRoom, this.userManager.GetUserId(User), id);

            if (!isReserved)
            {
                var roomType = this.roomService.GetRoom(id).TypeName;
                ModelState.AddModelError(String.Empty, $"All {roomType} are reserved for this period of time.");
                return this.View();
            }

            return RedirectToAction("Details", "Hotels", new { id = roomService.GetHotelIdByRoomId(id) }); ///// Return to My Reservations When Done
        }
    }
}