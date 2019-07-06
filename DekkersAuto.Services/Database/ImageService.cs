using DekkersAuto.Database;
using DekkersAuto.Database.Models;
using DekkersAuto.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DekkersAuto.Services.Database
{
    public class ImageService : DbServiceBase
    {
        public ImageService(ApplicationDbContext db) : base(db)
        {
        }

        public async Task DeleteImageAsync(Guid imageId)
        {
            var image = await _db.Images.FindAsync(imageId);
            if (image != null)
            {
                _db.Images.Remove(image);
                await _db.SaveChangesAsync();
            }
        }
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


        public IEnumerable<ImageDetailsModel> GetListingImages(Guid listingId)
        {
            return _db.Images
                .Where(i => i.ListingId == listingId)
                .OrderByDescending(i => i.IsFeature)
                .Select(i => 
                    new ImageDetailsModel
                    {
                        ImageId = i.Id,
                        IsFeature = i.IsFeature,
                        ListingId = i.ListingId,
                        Source = i.ImageString
                    });
        }

    }
}
