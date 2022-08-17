using AutoMapper;
using AutoMapper.QueryableExtensions;
using HotelBooking.Core.Contracts;
using HotelBooking.Core.Models.Users;
using HotelBooking.Infrastructure.Data;
using HotelBooking.Infrastructure.Data.Identity;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Core.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public UserService(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<User> GetUserById(string id)
            => await this.context
                         .Users
                         .FindAsync(id);

        public async Task<UserEditViewModel> GetUserForEdit(string userId)
        {
            var user = await this.context
                            .Users
                            .FindAsync(userId);

            var projectedUser = this.mapper.Map<UserEditViewModel>(user);

            return projectedUser;
        }

        public async Task<IEnumerable<UserListViewModel>> GetUsers(string searchString = null)
        {
            IEnumerable<UserListViewModel> users;



            if (searchString == null)
            {
                users = await this.context
                    .Users
                    .ProjectTo<UserListViewModel>(this.mapper.ConfigurationProvider)
                    .ToListAsync();
            }
            else
            {
                users = await this.context
                    .Users
                    .Where(u => u.FirstName.ToLower().Contains(searchString.ToLower()) || u.LastName.ToLower().Contains(searchString.ToLower()))
                    .ProjectTo<UserListViewModel>(this.mapper.ConfigurationProvider)
                    .ToListAsync();
            }

            return users;
        }

        public async Task<bool> UpdateUser(UserEditViewModel model)
        {
            bool result = false;
            var user = await this.context
                                 .Users
                                 .FindAsync(model.Id);

            if (user != null)
            {
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;

                try
                {
                    this.context.SaveChanges();
                    result = true;
                }
                catch (Exception)
                {
                    result = false;
                }
            }

            return result;
        }
    }
}