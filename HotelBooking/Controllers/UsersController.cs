using HotelBooking.Core.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HotelBooking.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        public IActionResult Reservations()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var reservations = this.userService.GetReservationsByUserId(userId);

            return View(reservations);
        }
    }
}