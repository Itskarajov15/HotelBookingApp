using HotelBooking.Core.Models.Users;

namespace HotelBooking.Core.Contracts
{
    public interface IUserService
    {
        List<UserReservationViewModel> GetReservationsByUserId(string userId);

        Task<IEnumerable<UserListViewModel>> GetUsers();

        Task<UserEditViewModel> GetUserForEdit(string userId);

        Task<bool> UpdateUser(UserEditViewModel model);
    }
}