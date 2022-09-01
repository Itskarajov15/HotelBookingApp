using HotelBooking.Core.Models.Hotels;
using HotelBooking.Core.Models.Rooms;
using HotelBooking.Core.Models.Users;

namespace HotelBooking.Core.Contracts
{
    public interface IReservationService
    {
        List<UserReservationViewModel> GetReservationsByUserId(string userId);

        bool DeclineReservation(int reservationId);

        bool ReserveRoom(ReserveRoomViewModel model, string userId, int roomId);

        List<HotelCardViewModel> GetHotelsWithFreeRooms(FilterRoomsViewModel model);

        IEnumerable<int> GetTakenRoomsIds(FilterRoomsViewModel model);
    }
}