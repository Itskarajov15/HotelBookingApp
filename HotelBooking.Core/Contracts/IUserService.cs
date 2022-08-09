using HotelBooking.Core.Models.Users;

namespace HotelBooking.Core.Contracts
{
    public interface IUserService
    {
        List<UserReservationViewModel> GetReservationsByUserId(string userId);
    }
}