using AspNetCoreHero.ToastNotification.Abstractions;
using HotelBooking.Core.Constants;
using HotelBooking.Core.Contracts;
using HotelBooking.Core.Models.Users;
using HotelBooking.Infrastructure.Data.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
            if (!ModelState.IsValid)
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

        [HttpPost]
        public async Task<IActionResult> Roles(UserRolesViewModel model)
        {
            var user = await userService.GetUserById(model.UserId);
            var userRoles = await userManager.GetRolesAsync(user);
            await userManager.RemoveFromRolesAsync(user, userRoles);

            if (model.RoleNames.Length > 0)
            {
                await userManager.AddToRolesAsync(user, model.RoleNames);
            }

            return RedirectToAction(nameof(ManageUsers));
        }

        public async Task<IActionResult> Roles(string id)
        {
            var user = await userService.GetUserById(id);
            var model = new UserRolesViewModel()
            {
                UserId = user.Id,
                Name = $"{user.FirstName} {user.LastName}"
            };

            ViewBag.RoleItems = roleManager.Roles
                                   .ToList()
                                   .Select(r => new SelectListItem()
                                   {
                                       Text = r.Name,
                                       Value = r.Name,
                                       Selected = userManager.IsInRoleAsync(user, r.Name).Result
                                   })
                                   .ToList();

            return View(model);
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