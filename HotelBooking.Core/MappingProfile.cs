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
                .ForMember(h => h.HotelImages, cfg => cfg.MapFrom(x => x.HotelImages.Select(x => x.Url).ToList()));

            this.CreateMap<Hotel, HotelCardViewModel>();

            this.CreateMap<AddHotelViewModel, Hotel>();

            this.CreateMap<City, HotelCityViewModel>();

            this.CreateMap<Hotel, AdminHotelViewModel>();

            //Rooms
            this.CreateMap<RoomType, RoomTypeViewModel>();

            this.CreateMap<Room, RoomViewModel>()
                .ForMember(r => r.RoomImageUrls, cfg => cfg.MapFrom(x => x.RoomImages.Select(i => i.Url).ToList()));

            this.CreateMap<Room, RoomCardViewModel>()
                .ForMember(r => r.RoomImageUrl, cfg => cfg.MapFrom(x => x.RoomImages.Select(i => i.Url).FirstOrDefault()));

            this.CreateMap<AddRoomViewModel, Room>();

            //Reservations
            this.CreateMap<Reservation, UserReservationViewModel>()
                .ForMember(r => r.HotelName, cfg => cfg.MapFrom(x => x.Room.Hotel.Name))
                .ForMember(r => r.StartDate, cfg => cfg.MapFrom(x => x.StartDate.ToString("d")))
                .ForMember(r => r.EndDate, cfg => cfg.MapFrom(x => x.EndDate.ToString("d")))
                .ForMember(r => r.GuestName, cfg => cfg.MapFrom(x => $"{x.User.FirstName} {x.User.LastName}"))
                .ForMember(r => r.RoomType, cfg => cfg.MapFrom(x => x.Room.RoomType.Name));

            //Users
            this.CreateMap<User, UserListViewModel>()
                .ForMember(u => u.Name, cfg => cfg.MapFrom(x => $"{x.FirstName} {x.LastName}"));

            this.CreateMap<User, UserEditViewModel>();
        }
    }
}