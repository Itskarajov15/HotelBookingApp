using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using HotelBooking.Core.Contracts;

namespace HotelBooking.Core.Services
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly Cloudinary cloudinary;

        public CloudinaryService(Cloudinary cloudinary)
        {
            this.cloudinary = cloudinary;
        }

        public async Task<string> UploadPicture(string imageUrl)
        {
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(imageUrl),
                UseFilename = true,
                UniqueFilename = false,
                Overwrite = true
            };

            var uploadResult = await cloudinary.UploadAsync(uploadParams);
            string publicId = uploadResult.JsonObj["public_id"].ToString();
            var uploadedImageUrl = await cloudinary.GetResourceAsync(publicId);

            return uploadedImageUrl.Url;
        }
    }
}