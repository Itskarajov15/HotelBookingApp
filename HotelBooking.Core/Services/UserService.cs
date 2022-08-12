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

        public List<UserReservationViewModel> GetReservationsByUserId(string userId)
            => this.context
                   .Reservations
                   .Where(r => r.UserId == userId)
                   .ProjectTo<UserReservationViewModel>(this.mapper.ConfigurationProvider)
                   .ToList();

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

        public async Task<IEnumerable<UserListViewModel>> GetUsers()
            => await this.context
                             .Users
                             .ProjectTo<UserListViewModel>(this.mapper.ConfigurationProvider)
                             .ToListAsync();

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