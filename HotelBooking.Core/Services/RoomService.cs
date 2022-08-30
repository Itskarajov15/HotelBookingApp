using AutoMapper;
using AutoMapper.QueryableExtensions;
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
        private readonly IMapper mapper;
        private readonly ICloudinaryService cloudinaryService;

        public RoomService(ApplicationDbContext context, IMapper mapper, ICloudinaryService cloudinaryService)
        {
            this.context = context;
            this.mapper = mapper;
            this.cloudinaryService = cloudinaryService;
        }

        public async Task<bool> AddRoom(AddRoomViewModel room)
        {
            bool isAdded = false;

            var newRoom = mapper.Map<Room>(room);

            foreach (var image in room.Images)
            {
                newRoom.RoomImages.Add(new RoomImage()
                {
                    Url = await this.cloudinaryService.UploadPicture(image)
                });
            }

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
                   .ProjectTo<RoomCardViewModel>(this.mapper.ConfigurationProvider)
                   .ToList()
                   .DistinctBy(r => r.RoomTypeName)
                   .ToList();

        public int GetHotelIdByRoomId(int roomId)
            => this.context
                   .Rooms
                   .FirstOrDefault(r => r.Id == roomId)
                   .HotelId;

        public RoomViewModel GetRoom(int roomId)
            => this.context
                   .Rooms
                   .Where(r => r.Id == roomId)
                   .ProjectTo<RoomViewModel>(this.mapper.ConfigurationProvider)
                   .FirstOrDefault();

        public IEnumerable<RoomTypeViewModel> GetRoomTypes()
            => this.context
                   .RoomTypes
                   .ProjectTo<RoomTypeViewModel>(this.mapper.ConfigurationProvider)
                   .ToList();
    }
}