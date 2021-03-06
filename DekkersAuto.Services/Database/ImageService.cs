﻿using DekkersAuto.Database;
using DekkersAuto.Database.Models;
using DekkersAuto.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//I, Christopher Dekker, student number 000311337, certify that all code
//submitted is my own work; that I have not copied it from any other source
//I also certify that I have not allowed by work to be copied by others
namespace DekkersAuto.Services.Database
{
    /// <summary>
    /// Service for handling all image related db interactions
    /// </summary>
    public class ImageService : DbServiceBase
    {
        public ImageService(ApplicationDbContext db) : base(db)
        {
        }

        /// <summary>
        /// Deletes pan image by image id
        /// </summary>
        /// <param name="imageId"></param>
        /// <returns></returns>
        public async Task DeleteImageAsync(Guid imageId)
        {
            var image = await _db.Images.FindAsync(imageId);
            if (image != null)
            {
                _db.Images.Remove(image);
                await _db.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Sets the feature image for a listing
        /// </summary>
        /// <param name="imageId"></param>
        /// <param name="listingId"></param>
        /// <returns></returns>
        public async Task SetFeatureImage(Guid imageId, Guid listingId)
        {
            var listingImages = _db.Images.Where(i => i.ListingId == listingId);

            var featureImage = listingImages.SingleOrDefault(i => i.IsFeature);

            if (featureImage != null && featureImage.Id != imageId)
            {
                featureImage.IsFeature = false;
                _db.Images.Update(featureImage);
                await _db.SaveChangesAsync();
            }

            var selectedImage = await _db.Images.FindAsync(imageId);
            selectedImage.IsFeature = !selectedImage.IsFeature;
            _db.Images.Update(selectedImage);
            await _db.SaveChangesAsync();
        }

        /// <summary>
        /// Adds an image to a listing
        /// </summary>
        /// <param name="listingId"></param>
        /// <param name="image"></param>
        /// <returns></returns>
        public async Task<Image> AddImageToListingAsync(Guid listingId, string image)
        {
            var listingImage = await _db.Images.AddAsync(new Image
            {
                ImageString = image,
                ListingId = listingId
            });
            await _db.SaveChangesAsync();

            return listingImage.Entity;
        }

        /// <summary>
        /// Gets the images for a listing
        /// </summary>
        /// <param name="listingId"></param>
        /// <returns></returns>
        public IEnumerable<ImageModel> GetListingImages(Guid listingId)
        {
            return _db.Images
                .Where(i => i.ListingId == listingId)
                .OrderByDescending(i => i.IsFeature)
                .Select(i =>
                    new ImageModel
                    {
                        Id = i.Id,
                        IsFeature = i.IsFeature,
                        ListingId = i.ListingId,
                        Source = i.ImageString
                    });
        }

    }
}
