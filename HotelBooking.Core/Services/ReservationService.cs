using AutoMapper;
using AutoMapper.QueryableExtensions;
using HotelBooking.Core.Contracts;
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

        public List<RoomCardViewModel> GetFreeRooms(FilterRoomsViewModel model)
        {
            var takenRoomIds = this.context
                                   .Reservations
                                   .Where(r => (model.StartDate >= r.StartDate && model.StartDate < r.EndDate) || (model.EndDate >= r.StartDate && model.EndDate <= r.EndDate))
                                   .Where(r => r.Room.RoomType.CountOfPeople == model.CountOfPeople)
                                   .Select(r => r.RoomId)
                                   .ToList();

            var freeRooms = this.context
                                .Rooms
                                .Where(r => r.RoomType.CountOfPeople == model.CountOfPeople)
                                .Where(r => !takenRoomIds.Contains(r.Id))
                                .ProjectTo<RoomCardViewModel>(this.mapper.ConfigurationProvider)
                                .ToList()
                                .DistinctBy(r => r.RoomTypeName)
                                .ToList();

            return freeRooms;
        }

        public List<UserReservationViewModel> GetReservationsByUserId(string userId)
            => this.context
            .Reservations
            .Where(r => r.UserId == userId)
            .ProjectTo<UserReservationViewModel>(this.mapper.ConfigurationProvider)
            .ToList();
    }
}