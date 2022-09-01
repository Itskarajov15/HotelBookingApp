using HotelBooking.Core.Models.Hotels;

namespace HotelBooking.Core.Contracts
{
    public interface IHotelService
    {
        IEnumerable<HotelCardViewModel> GetHotels(string searchString = null);

        Task<bool> AddHotel(AddHotelViewModel hotel);

        HotelViewModel GetHotel(int id);

        IEnumerable<AdminHotelViewModel> GetHotelsForManaging(string searchString = null);
    }
}