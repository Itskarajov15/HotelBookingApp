using HotelBooking.Core.Contracts;
using HotelBooking.Core.Models.Rooms;
using HotelBooking.Infrastructure.Data;
using HotelBooking.Infrastructure.Data.Models;
using System.Globalization;

namespace HotelBooking.Core.Services
{
    public class RoomService : IRoomService
    {
        private readonly ApplicationDbContext context;

        public RoomService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public bool ReserveRoom(ReserveRoomViewModel model, string userId, int roomId)
        {
            var isReservationDone = false;
            var roomType = GetRoomTypeByRoomId(roomId);
            var hotelId = this.context
                              .Rooms
                              .FirstOrDefault(r => r.Id == roomId)
                              .HotelId;

            var freeRoom = GetFreeRoom(model.StartDate, model.EndDate, roomType, hotelId);

            if (freeRoom == null)
            {
                return isReservationDone;
            }

            var reservation = new Reservation()
            {
                UserId = userId,
                EndDate = model.EndDate,
                RoomId = freeRoom.Id,
                StartDate = model.StartDate,
                TotalPrice = (decimal)((model.EndDate - model.StartDate).TotalDays) * freeRoom.PriceForOneNight
            };

            try
            {
                this.context.Reservations.Add(reservation);
                this.context.SaveChanges();
                isReservationDone = true;
            }
            catch (Exception)
            {
                isReservationDone = false;
            }

            return isReservationDone;
        }

        private string GetRoomTypeByRoomId(int roomId)
            => this.context
                .Rooms
                .Where(r => r.Id == roomId)
                .Select(r => r.RoomType.TypeName)
                .FirstOrDefault();

        private Room GetFreeRoom(DateTime startDate, DateTime endDate, string typeName, int hotelId)
        {
            var takenRoomIds = this.context
                             .Reservations
                             .Where(r => r.Room.RoomType.TypeName == typeName)
                             .Where(r => r.Room.HotelId == hotelId)
                             .Where(r => (startDate >= r.StartDate && startDate < r.EndDate) || (endDate >= r.StartDate && endDate <= r.EndDate))
                             .Select(r => r.RoomId)
                             .ToList();

            var freeRoom = this.context
                            .Rooms
                            .Where(r => !takenRoomIds.Contains(r.Id)
                            && r.RoomType.TypeName == typeName
                            && r.HotelId == hotelId)
                            .FirstOrDefault();

            return freeRoom;
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