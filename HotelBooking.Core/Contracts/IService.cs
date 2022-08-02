namespace HotelBooking.Core.Contracts
{
    public interface IService
    {
        bool ValidateDates(DateTime startDate, DateTime endDate);
    }
}