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
        private readonly IRoomService service;
        private readonly UserManager<User> userManager;

        public RoomsController(IRoomService service, UserManager<User> userManager)
        {
            this.service = service;
            this.userManager = userManager;
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

            if (!ValidateDates(reserveRoom.StartDate, reserveRoom.EndDate))
            {
                ModelState.AddModelError(String.Empty, "Invalid dates");
                return this.View();
            }

            var isReserved = this.service.ReserveRoom(reserveRoom, this.userManager.GetUserId(User), id);

            if (!isReserved)
            {
                var roomType = this.service.GetRoom(id).TypeName;
                ModelState.AddModelError(String.Empty, $"All {roomType} are reserved for this period of time.");
                return this.View();
            }

            return this.RedirectToAction("All", "Hotels");
        }

        private bool ValidateDates(DateTime startDate, DateTime endDate)
        {
            if (endDate.Date < startDate.Date 
                || startDate.Date < DateTime.UtcNow.Date 
                || startDate.Year > DateTime.UtcNow.Year 
                || endDate.Year > DateTime.UtcNow.Year)
            {
                return false;
            }

            return true;
        }
    }
}