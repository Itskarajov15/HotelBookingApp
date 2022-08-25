using HotelBooking.Core.Models.Rooms;
using HotelBooking.Core.Models.Users;

namespace HotelBooking.Core.Contracts
{
    public interface IReservationService
    {
        List<UserReservationViewModel> GetReservationsByUserId(string userId);

        bool DeclineReservation(int reservationId);

        List<RoomCardViewModel> GetFreeRooms(FilterRoomsViewModel model);
    }
}