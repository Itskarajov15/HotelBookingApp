using AspNetCoreHero.ToastNotification.Abstractions;
using HotelBooking.Core.Contracts;
using HotelBooking.Core.Models.Rooms;
using HotelBooking.Core.Services;
using HotelBooking.Infrastructure.Data.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HotelBooking.Controllers
{
    public class ReservationsController : BaseController
    {
        private readonly IReservationService reservationService;
        private readonly INotyfService notyfService;
        private readonly UserManager<User> userManager;
        private readonly IService service;
        private readonly IRoomService roomService;

        public ReservationsController(IReservationService reservationService,
            INotyfService notyfService,
            UserManager<User> userManager,
            IService service,
            IRoomService roomService)
        {
            this.reservationService = reservationService;
            this.notyfService = notyfService;
            this.userManager = userManager;
            this.service = service;
            this.roomService = roomService;
        }

        public IActionResult MyReservations()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var reservations = this.reservationService.GetReservationsByUserId(userId);

            return View(reservations);
        }

        public IActionResult DeclineReservation(int id)
        {
            var isDeclined = this.reservationService.DeclineReservation(id);

            if (isDeclined)
            {
                notyfService.Success("Reservation is declined successfully");
            }
            else
            {
                notyfService.Error("There is a problem with declining reservation");
            }

            return RedirectToAction(nameof(MyReservations));
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

            return RedirectToAction("MyReservations", "Reservations");
        }
    }
}