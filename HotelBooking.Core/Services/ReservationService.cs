using AutoMapper;
using AutoMapper.QueryableExtensions;
using HotelBooking.Core.Contracts;
using HotelBooking.Core.Models.Hotels;
using HotelBooking.Core.Models.Rooms;
using HotelBooking.Core.Models.Users;
using HotelBooking.Infrastructure.Data;
using HotelBooking.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace HotelBooking.Core.Services
{
    public class ReservationService : IReservationService
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public ReservationService(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public bool DeclineReservation(int reservationId)
        {
            var isDeclined = false;

            var reservation = this.context
                                  .Reservations
                                  .Find(reservationId);

            try
            {
                this.context.Remove(reservation);
                this.context.SaveChanges();
                isDeclined = true;
            }
            catch (Exception)
            {
                isDeclined = false;
            }

            return isDeclined;
        }

        public IEnumerable<int> GetTakenRoomsIds(FilterRoomsViewModel model)
            => this.context
                   .Reservations
                   .Where(r => (model.StartDate >= r.StartDate && model.StartDate < r.EndDate) || (model.EndDate >= r.StartDate && model.EndDate <= r.EndDate))
                   .Where(r => r.Room.RoomType.CountOfPeople == model.CountOfPeople)
                   .Where(r => r.Room.Hotel.City.Id == model.CityId)
                   .Select(r => r.RoomId)
                   .ToList();

        public List<HotelCardViewModel> GetHotelsWithFreeRooms(FilterRoomsViewModel model)
        {
            var takenRoomIds = GetTakenRoomsIds(model);

            var freeRoomsIds = this.context
                                .Rooms
                                .Where(r => r.Hotel.CityId == model.CityId)
                                .Where(r => r.RoomType.CountOfPeople == model.CountOfPeople)
                                .Where(r => !takenRoomIds.Contains(r.Id))
                                .Select(r => r.Id)
                                .ToList();

            var hotels = this.context
                             .Hotels
                             .Where(h => h.Rooms.Any(r => freeRoomsIds.Contains(r.Id) && h.CityId == model.CityId))
                             .ProjectTo<HotelCardViewModel>(this.mapper.ConfigurationProvider)
                             .ToList();
                             

            return hotels;
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
                .Select(r => r.RoomType.Name)
                .FirstOrDefault();

        private Room GetFreeRoom(DateTime startDate, DateTime endDate, string typeName, int hotelId)
        {
            var takenRoomIds = this.context
                             .Reservations
                             .Where(r => r.Room.RoomType.Name == typeName)
                             .Where(r => r.Room.HotelId == hotelId)
                             .Where(r => (startDate >= r.StartDate && startDate < r.EndDate) || (endDate >= r.StartDate && endDate <= r.EndDate))
                             .Select(r => r.RoomId)
                             .ToList();

            var freeRoom = this.context
                            .Rooms
                            .Where(r => !takenRoomIds.Contains(r.Id)
                            && r.RoomType.Name == typeName
                            && r.HotelId == hotelId)
                            .FirstOrDefault();

            return freeRoom;
        }

        public List<UserReservationViewModel> GetReservationsByUserId(string userId)
            => this.context
            .Reservations
            .Where(r => r.UserId == userId)
            .ProjectTo<UserReservationViewModel>(this.mapper.ConfigurationProvider)
            .ToList();
    }
}