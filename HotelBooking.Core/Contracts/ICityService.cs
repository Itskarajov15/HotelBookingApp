using HotelBooking.Core.Models.Hotels;

namespace HotelBooking.Core.Contracts
{
    public interface ICityService
    {
        IEnumerable<HotelCityViewModel> GetCityNames();

        bool IsCityValid(int cityId);
    }
}