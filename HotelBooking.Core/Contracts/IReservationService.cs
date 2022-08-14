using HotelBooking.Core.Models.Users;

namespace HotelBooking.Core.Contracts
{
    public interface IReservationService
    {
        List<UserReservationViewModel> GetReservationsByUserId(string userId);
    }
}