﻿using CloudinaryDotNet.Actions;
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

        public async Task<string> UploadPicture(IFormFile picture)
        {
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(picture.FileName, picture.OpenReadStream()),
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