using HotelBooking.Core.Models;

namespace HotelBooking.Core.Contracts
{
    public interface IHotelService
    {
        IEnumerable<HotelCardViewModel> GetAllHotels();
    }
}