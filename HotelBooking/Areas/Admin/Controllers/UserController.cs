using HotelBooking.Core.Constants;
using HotelBooking.Core.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static HotelBooking.Core.Constants.UserConstants;
using System.Security.Claims;
using HotelBooking.Infrastructure.Data.Identity;
using HotelBooking.Core.Models.Users;
using AspNetCoreHero.ToastNotification.Abstractions;

namespace HotelBooking.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserService userService;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<User> userManager;
        private readonly INotyfService notyfService;

        public UserController(IUserService userService,
            RoleManager<IdentityRole> roleManager,
            UserManager<User> userManager,
            INotyfService notyfService)
        {
            this.userService = userService;
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.notyfService = notyfService;
        }

        [Authorize(Roles = UserConstants.Roles.Administrator)]
        public async Task<IActionResult> ManageUsers()
        {
            var users = await userService.GetUsers();

            return View(users);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var model = await userService.GetUserForEdit(id);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, UserEditViewModel model)
        {
            if (!ModelState.IsValid || id != model.Id)
            {
                return this.View(model);
            }

            if (await userService.UpdateUser(model))
            {
                notyfService.Success("User is edited successfully!");
            }
            else
            {
                notyfService.Error("There is a problem with the edit");
            }

            return View(model);
        }

        public async Task<IActionResult> Roles(string id)
        {
            return Ok(id);
        }

        public IActionResult Reservations()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var reservations = this.userService.GetReservationsByUserId(userId);

            return View(reservations);
        }

        //public async Task<IActionResult> CreateRole()
        //{
        //    await roleManager.CreateAsync(new IdentityRole()
        //    {
        //        Name = "Administrator"
        //    });

        //    return Ok();
        //}
    }
}