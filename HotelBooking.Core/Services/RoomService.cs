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

        public IEnumerable<RoomViewModel> GetAllRooms()
            => this.context
                   .Rooms
                   .Select(r => new RoomViewModel()
                   {
                       RoomType = r.RoomType.TypeName,
                       RoomDescription = r.Description,
                       RoomImageUrl = r.RoomImages.Select(i => i.ImageUrl).FirstOrDefault()
                   })
                   .ToList();

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