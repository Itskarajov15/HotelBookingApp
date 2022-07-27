using HotelBooking.Core.Contracts;
using HotelBooking.Core.Models.Rooms;
using HotelBooking.Infrastructure.Data;
using HotelBooking.Infrastructure.Data.Models;

namespace HotelBooking.Core.Services
{
    public class RoomService : IRoomService
    {
        private readonly ApplicationDbContext context;

        public RoomService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public bool ReserveRoom(ReserveRoomViewModel model)
        {
            var roomType = GetRoomTypeByRoomId(model.RoomId);

            if (IsRoomReserved(model.StartDate, model.EndDate, model.RoomId, roomType))
            {
                return false;
            }

            return true;
        }

        private string GetRoomTypeByRoomId(int roomId)
            => this.context
                .Rooms
                .Where(r => r.Id == roomId)
                .Select(r => r.RoomType.TypeName)
                .FirstOrDefault();

        private bool IsRoomReserved(DateTime startDate, DateTime endDate, int roomId, string typeName)
        {
            bool isReserved = this.context
                                  .Reservations
                                  .Where(r => r.Room.RoomType.TypeName == typeName)
                                  .Any(r => (r.StartDate < startDate && r.EndDate < startDate) || (r.StartDate > r.EndDate && r.EndDate > endDate));

            return isReserved;
        }

        public bool AddRoom(AddRoomViewModel room)
        {
            bool isAdded = false;

            var newRoom = new Room()
            {
                RoomTypeId = room.RoomTypeId,
                Description = room.Description,
                HotelId = room.HotelId,
                PriceForOneNight = room.PriceForOneNight
            };

            AddImageToRoom(newRoom, room.FirstRoomImageUrl);
            AddImageToRoom(newRoom, room.SecondRoomImageUrl);
            AddImageToRoom(newRoom, room.ThirdRoomImageUrl);

            try
            {
                this.context.Rooms.Add(newRoom);
                this.context.SaveChanges();
                isAdded = true;
            }
            catch (Exception)
            {
                isAdded = false;
            }

            return isAdded;
        }

        public IEnumerable<RoomCardViewModel> GetAllRoomsByHotel(int hotelId)
            => this.context
                   .Rooms
                   .Where(r => r.HotelId == hotelId)
                   .Select(r => new RoomCardViewModel()
                   {
                       Id = r.Id,
                       RoomType = r.RoomType.TypeName,
                       RoomDescription = r.Description,
                       RoomImageUrl = r.RoomImages.Select(i => i.ImageUrl).FirstOrDefault()
                   })
                   .ToList()
                   .DistinctBy(r => r.RoomType)
                   .ToList();

        public RoomViewModel GetRoom(int roomId)
            => this.context
                   .Rooms
                   .Where(r => r.Id == roomId)
                   .Select(r => new RoomViewModel()
                   {
                       Id = r.Id,
                       Description = r.Description,
                       HotelName = r.Hotel.HotelName,
                       PriceForOneNight = r.PriceForOneNight,
                       RoomImageUrls = r.RoomImages.Select(i => i.ImageUrl).ToList(),
                       TypeName = r.RoomType.TypeName
                   })
                   .FirstOrDefault();

        public IEnumerable<RoomTypeViewModel> GetRoomTypes()
            => this.context
                   .RoomTypes
                   .Select(rt => new RoomTypeViewModel
                   {
                       Id = rt.Id,
                       Name = rt.TypeName
                   })
                   .ToList();

        private void AddImageToRoom(Room room, string imageUrl)
            => room.RoomImages.Add(new RoomImage()
            {
                RoomId = room.Id,
                ImageUrl = imageUrl
            });
    }
}