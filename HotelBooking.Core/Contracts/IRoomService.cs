using HotelBooking.Core.Models.Rooms;

namespace HotelBooking.Core.Contracts
{
    public interface IRoomService
    {
        Task<bool> AddRoom(AddRoomViewModel room);

        IEnumerable<RoomTypeViewModel> GetRoomTypes();

        IEnumerable<RoomCardViewModel> GetAllRoomsByHotel(int hotelId);

        RoomViewModel GetRoom(int roomId);

        bool ReserveRoom(ReserveRoomViewModel model, string userId, int roomId);

        int GetHotelIdByRoomId(int roomId);
    }
}