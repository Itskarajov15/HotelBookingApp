using AutoMapper;
using AutoMapper.QueryableExtensions;
using HotelBooking.Core.Contracts;
using HotelBooking.Core.Models.Hotels;
using HotelBooking.Infrastructure.Data;

namespace HotelBooking.Core.Services
{
    public class CityService : ICityService
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public CityService(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public IEnumerable<HotelCityViewModel> GetCityNames()
            => this.context
           .Cities
           .ProjectTo<HotelCityViewModel>(this.mapper.ConfigurationProvider)
           .ToList();

        public bool IsCityValid(int cityId)
            => this.context.Cities.Any(c => c.Id == cityId) ? true : false;
    }
}