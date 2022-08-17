using AutoMapper;
using AutoMapper.QueryableExtensions;
using HotelBooking.Core.Contracts;
using HotelBooking.Core.Models.Rooms;
using HotelBooking.Core.Models.Users;
using HotelBooking.Infrastructure.Data;

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

        public List<UserReservationViewModel> GetReservationsByUserId(string userId)
            => this.context
            .Reservations
            .Where(r => r.UserId == userId)
            .ProjectTo<UserReservationViewModel>(this.mapper.ConfigurationProvider)
            .ToList();
    }
}