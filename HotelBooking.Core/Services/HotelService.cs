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

        public HotelService(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public bool AddHotel(AddHotelViewModel hotel)
        {
            bool isAdded = false;

            var newHotel = mapper.Map<Hotel>(hotel);

            var additionalImages = new List<HotelImage>();

            additionalImages.Add(new HotelImage()
            {
                ImageUrl = hotel.FirstAdditionalImageUrl
            });

            additionalImages.Add(new HotelImage()
            {
                ImageUrl = hotel.SecondAdditionalImageUrl
            });

            newHotel.HotelImages = additionalImages;

            try
            {
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

        public IEnumerable<HotelCityViewModel> GetCityNames()
            => this.context
                   .Cities
                   .ProjectTo<HotelCityViewModel>(this.mapper.ConfigurationProvider)
                   .ToList();

        public bool IsCityValid(int cityId)
            => this.context.Cities.Any(c => c.Id == cityId) ? true : false;

        public IEnumerable<HotelCardViewModel> GetHotelsBySearchString(string searchString)
            => this.context
                   .Hotels
                   .Where(h => h.HotelName.ToLower().Contains(searchString.ToLower()))
                   .ProjectTo<HotelCardViewModel>(this.mapper.ConfigurationProvider)
                   .ToList();
    }
}