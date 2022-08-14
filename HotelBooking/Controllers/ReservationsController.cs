using AspNetCoreHero.ToastNotification.Abstractions;
using HotelBooking.Core.Contracts;
using HotelBooking.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HotelBooking.Controllers
{
    public class ReservationsController : BaseController
    {
        private readonly IReservationService reservationService;
        private readonly INotyfService notyfService;

        public ReservationsController(IReservationService reservationService, INotyfService notyfService)
        {
            this.reservationService = reservationService;
            this.notyfService = notyfService;
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
    }
}