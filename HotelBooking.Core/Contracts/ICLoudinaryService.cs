namespace HotelBooking.Core.Contracts
{
    public interface ICloudinaryService
    {
        Task<string> UploadPicture(string imageUrl);
    }
}