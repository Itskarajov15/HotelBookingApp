using Microsoft.AspNetCore.Http;

namespace HotelBooking.Core.Contracts
{
    public interface ICloudinaryService
    {
        Task<string> UploadPicture(IFormFile picture);
    }
}