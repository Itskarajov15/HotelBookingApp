using HotelBooking.Core.Models.Hotels;

namespace HotelBooking.Core.Contracts
{
    public interface IHotelService
    {
        IEnumerable<HotelCardViewModel> GetAllHotels();

        IEnumerable<HotelCardViewModel> GetHotelsBySearchString(string searchString);

        IEnumerable<HotelCityViewModel> GetCityNames();

        bool IsCityValid(int cityId);

        bool AddHotel(AddHotelViewModel hotel);

        HotelViewModel GetHotel(int id);
    }
}