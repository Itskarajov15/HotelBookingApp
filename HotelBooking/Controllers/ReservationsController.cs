using HotelBooking.Core.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HotelBooking.Controllers
{
    public class ReservationsController : BaseController
    {
        private readonly IReservationService reservationService;

        public ReservationsController(IReservationService reservationService)
        {
            this.reservationService = reservationService;
        }

        public IActionResult MyReservations()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var reservations = this.reservationService.GetReservationsByUserId(userId);

            return View(reservations);
        }
    }
}