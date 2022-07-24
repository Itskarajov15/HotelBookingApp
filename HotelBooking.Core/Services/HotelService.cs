using HotelBooking.Core.Contracts;
using HotelBooking.Core.Models.Hotels;
using HotelBooking.Infrastructure.Data;
using HotelBooking.Infrastructure.Data.Models;

namespace HotelBooking.Core.Services
{
    public class HotelService : IHotelService
    {
        private readonly ApplicationDbContext context;

        public HotelService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public bool AddHotel(AddHotelViewModel hotel)
        {
            bool isAdded = false;

            var newHotel = new Hotel()
            {
                HotelName = hotel.HotelName,
                Description = hotel.Description,
                CityId = hotel.CityId,
                PrimaryImageUrl = hotel.PrimaryImageUrl
            };

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
            =>  this.context
                    .Hotels
                    .Where(h => h.Id == hotelId)
                    .Select(h => new HotelViewModel()
                    {
                        Id = h.Id,
                        HotelName = h.HotelName,
                        CityName = h.City.CityName,
                        CountryName = h.City.Country.CountryName,
                        Description = h.Description,
                        HotelImages = h.HotelImages.Select(h => h.ImageUrl).ToList(),
                        PrimaryImageUrl = h.PrimaryImageUrl
                    })
                    .FirstOrDefault();

        public IEnumerable<HotelCardViewModel> GetAllHotels()
            => this.context.Hotels
                        .Select(h => new HotelCardViewModel
                        {
                            Id = h.Id,
                            HotelName = h.HotelName,
                            PrimaryImageUrl = h.PrimaryImageUrl
                        })
                        .ToList();

        public IEnumerable<HotelCityViewModel> GetCityNames()
            => this.context
                   .Cities
                   .Select(c => new HotelCityViewModel
                   {
                       Id = c.Id,
                       Name = c.CityName
                   })
                   .ToList();

        public bool IsCityValid(int cityId)
            => this.context.Cities.Any(c => c.Id == cityId) ? true : false;
    }
}