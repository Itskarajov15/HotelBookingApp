using HotelBooking.Core.Models.Rooms;

namespace HotelBooking.Core.Contracts
{
    public interface IRoomService
    {
        bool Add(AddRoomViewModel room);

        IEnumerable<RoomTypeViewModel> GetRoomTypes();
    }
}