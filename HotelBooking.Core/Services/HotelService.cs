using HotelBooking.Core.Contracts;
using HotelBooking.Core.Models.Hotels;
using HotelBooking.Infrastructure.Data;

namespace HotelBooking.Core.Services
{
    public class HotelService : IHotelService
    {
        private readonly ApplicationDbContext context;

        public HotelService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<HotelCardViewModel> GetAllHotels()
            => this.context.Hotels
                        .Select(h => new HotelCardViewModel
                        {
                            Id = h.Id,
                            HotelName = h.HotelName,
                            PrimaryImageUrl = h.PrimaryImageUrl
                        })
                        .ToList();
    }
}