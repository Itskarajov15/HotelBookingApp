using AutoMapper;
using HotelBooking.Core.Models.Hotels;
using HotelBooking.Core.Models.Rooms;
using HotelBooking.Core.Models.Users;
using HotelBooking.Infrastructure.Data.Identity;
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

            this.CreateMap<AddHotelViewModel, Hotel>();

            this.CreateMap<City, HotelCityViewModel>()
                .ForMember(c => c.Name, cfg => cfg.MapFrom(x => x.CityName));

            //Rooms
            this.CreateMap<RoomType, RoomTypeViewModel>();

            this.CreateMap<Room, RoomViewModel>()
                .ForMember(r => r.HotelName, cfg => cfg.MapFrom(x => x.Hotel.HotelName))
                .ForMember(r => r.TypeName, cfg => cfg.MapFrom(x => x.RoomType.TypeName))
                .ForMember(r => r.RoomImageUrls, cfg => cfg.MapFrom(x => x.RoomImages.Select(i => i.ImageUrl).ToList()));

            this.CreateMap<Room, RoomCardViewModel>()
                .ForMember(r => r.RoomType, cfg => cfg.MapFrom(x => x.RoomType.TypeName))
                .ForMember(r => r.RoomDescription, cfg => cfg.MapFrom(x => x.Description))
                .ForMember(r => r.RoomImageUrl, cfg => cfg.MapFrom(x => x.RoomImages.Select(i => i.ImageUrl).FirstOrDefault()));

            this.CreateMap<AddRoomViewModel, Room>();

            //Reservations
            this.CreateMap<Reservation, UserReservationViewModel>()
                .ForMember(r => r.HotelName, cfg => cfg.MapFrom(x => x.Room.Hotel.HotelName))
                .ForMember(r => r.StartDate, cfg => cfg.MapFrom(x => x.StartDate.ToString("d")))
                .ForMember(r => r.EndDate, cfg => cfg.MapFrom(x => x.EndDate.ToString("d")))
                .ForMember(r => r.GuestName, cfg => cfg.MapFrom(x => $"{x.User.FirstName} {x.User.LastName}"))
                .ForMember(r => r.RoomType, cfg => cfg.MapFrom(x => x.Room.RoomType.TypeName))
                .ForMember(r => r.ReservationId, cfg => cfg.MapFrom(x => x.Id));

            //Users
            this.CreateMap<User, UserListViewModel>()
                .ForMember(u => u.Name, cfg => cfg.MapFrom(x => $"{x.FirstName} {x.LastName}"));

            this.CreateMap<User, UserEditViewModel>();
        }
    }
}