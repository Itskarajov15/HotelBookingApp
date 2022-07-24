using HotelBooking.Core.Models.Rooms;

namespace HotelBooking.Core.Contracts
{
    public interface IRoomService
    {
        bool AddRoom(AddRoomViewModel room);

        IEnumerable<RoomTypeViewModel> GetRoomTypes();

        IEnumerable<RoomViewModel> GetAllRooms();
    }
}