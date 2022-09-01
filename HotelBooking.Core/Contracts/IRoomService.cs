using HotelBooking.Core.Models.Rooms;

namespace HotelBooking.Core.Contracts
{
    public interface IRoomService
    {
        Task<bool> AddRoom(AddRoomViewModel room);

        IEnumerable<RoomTypeViewModel> GetRoomTypes();

        IEnumerable<RoomCardViewModel> GetAllRoomsByHotel(int hotelId, FilterRoomsViewModel filterModel = null);

        RoomViewModel GetRoom(int roomId);

        int GetHotelIdByRoomId(int roomId);
    }
}