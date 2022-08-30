using AutoMapper;
using AutoMapper.QueryableExtensions;
using HotelBooking.Core.Contracts;
using HotelBooking.Core.Models.Hotels;
using HotelBooking.Infrastructure.Data;
using HotelBooking.Infrastructure.Data.Models;

namespace HotelBooking.Core.Services
{
    public class HotelService : IHotelService
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly ICloudinaryService cloudinaryService;

        public HotelService(ApplicationDbContext context,
            IMapper mapper,
            ICloudinaryService cloudinaryService)
        {
            this.context = context;
            this.mapper = mapper;
            this.cloudinaryService = cloudinaryService;
        }

        public async Task<bool> AddHotel(AddHotelViewModel hotel)
        {
            bool isAdded = false;

            var newHotel = mapper.Map<Hotel>(hotel);

            try
            {
                newHotel.PrimaryImageUrl = await this.cloudinaryService.UploadPicture(hotel.PrimaryImage);

                foreach (var picture in hotel.AdditionalImages)
                {
                    newHotel.HotelImages.Add(new HotelImage()
                    {
                        Url = await this.cloudinaryService.UploadPicture(picture)
                    });
                }

                this.context.Hotels.Add(newHotel);
                this.context.SaveChanges();
                isAdded = true;
            }
            catch (Exception)
            {
                isAdded = false;
            }

            return isAdded;
        }

        public HotelViewModel GetHotel(int hotelId)
            => this.context
                    .Hotels
                    .Where(h => h.Id == hotelId)
                    .ProjectTo<HotelViewModel>(this.mapper.ConfigurationProvider)
                    .FirstOrDefault();

        public IEnumerable<HotelCardViewModel> GetAllHotels()
            => this.context.Hotels
                        .ProjectTo<HotelCardViewModel>(this.mapper.ConfigurationProvider)
                        .ToList();

        public IEnumerable<HotelCardViewModel> GetHotelsBySearchString(string searchString)
            => this.context
                   .Hotels
                   .Where(h => h.Name.ToLower().Contains(searchString.ToLower()))
                   .ProjectTo<HotelCardViewModel>(this.mapper.ConfigurationProvider)
                   .ToList();

        public IEnumerable<AdminHotelViewModel> GetHotelsForManaging(string searchString)
        {
            IEnumerable<AdminHotelViewModel> hotels;

            if (searchString == null)
            {
                hotels = this.context
                             .Hotels
                             .ProjectTo<AdminHotelViewModel>(this.mapper.ConfigurationProvider)
                             .ToList();
            }
            else
            {
                hotels = this.context
                             .Hotels
                             .Where(h => h.Name.ToLower().Contains(searchString.ToLower()))
                             .ProjectTo<AdminHotelViewModel>(this.mapper.ConfigurationProvider)
                             .ToList();
            }

            return hotels;
        }
    }
}