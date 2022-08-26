using Microsoft.AspNetCore.Http;

namespace HotelBooking.Core.Contracts
{
    public interface ICloudinaryService
    {
        Task<List<string>> UploadPictures(List<IFormFile> imageBlob);
    }
}