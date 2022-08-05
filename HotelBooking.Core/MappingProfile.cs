using AutoMapper;
using HotelBooking.Core.Models.Hotels;
using HotelBooking.Infrastructure.Data.Models;

namespace HotelBooking.Core
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Hotels

            this.CreateMap<Hotel, HotelViewModel>()
                .ForMember(h => h.HotelImages, cfg => cfg.MapFrom(x => x.HotelImages.Select(x => x.ImageUrl).ToList()))
                .ForMember(h => h.CityName, cfg => cfg.MapFrom(x => x.City.CityName))
                .ForMember(h => h.CountryName, cfg => cfg.MapFrom(x => x.City.Country.CountryName));

            this.CreateMap<Hotel, HotelCardViewModel>();

            this.CreateMap<City, HotelCityViewModel>()
                .ForMember(c => c.Name, cfg => cfg.MapFrom(x => x.CityName));
        }
    }
}