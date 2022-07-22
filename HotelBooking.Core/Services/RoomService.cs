using HotelBooking.Core.Contracts;
using HotelBooking.Core.Models.Rooms;
using HotelBooking.Infrastructure.Data;

namespace HotelBooking.Core.Services
{
    public class RoomService : IRoomService
    {
        private readonly ApplicationDbContext context;

        public RoomService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public bool Add(AddRoomViewModel room)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RoomTypeViewModel> GetRoomTypes()
            => this.context
                   .RoomTypes
                   .Select(rt => new RoomTypeViewModel
                   {
                       Id = rt.Id,
                       Name = rt.TypeName
                   })
                   .ToList();
    }
}