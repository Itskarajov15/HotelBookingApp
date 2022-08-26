using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using HotelBooking.Core.Contracts;
using Microsoft.AspNetCore.Http;

namespace HotelBooking.Core.Services
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly Cloudinary cloudinary;

        public CloudinaryService(Cloudinary cloudinary)
        {
            this.cloudinary = cloudinary;
        }

        public async Task<List<string>> UploadPictures(List<IFormFile> imageBlob)
        {
            var urls = new List<string>();

            foreach (var item in imageBlob)
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(item.FileName, item.OpenReadStream()),
                    UseFilename = true,
                    UniqueFilename = false,
                    Overwrite = true
                };

                var uploadResult = await cloudinary.UploadAsync(uploadParams);
                string publicId = uploadResult.JsonObj["public_id"].ToString();
                var uploadedImageUrl = await cloudinary.GetResourceAsync(publicId);

                urls.Add(uploadedImageUrl.Url);
            }

            return urls;
        }
    }
}