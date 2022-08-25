using HotelBooking.Core.Contracts;

namespace HotelBooking.Core.Services
{
    public class Service : IService
    {
        public bool ValidateDates(DateTime startDate, DateTime endDate)
        {
            if (endDate.Date < startDate.Date
                || startDate.Date < DateTime.UtcNow.Date
                || startDate.Year > DateTime.UtcNow.Year
                || endDate.Year > DateTime.UtcNow.Year
                || startDate == endDate)
            {
                return false;
            }

            return true;
        }
    }
}