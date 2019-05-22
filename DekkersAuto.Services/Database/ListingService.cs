using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DekkersAuto.Database;
using DekkersAuto.Database.Models;
using DekkersAuto.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace DekkersAuto.Services.Database
{
    public class ListingService : DbServiceBase
    {
        public ListingService(ApplicationDbContext db) : base(db)
        {
        }
        
        public List<ListingListItemModel> GetActiveInventoryList()
        {
            return _db.Listings
                .Where(l => l.IsActive)
                .Include(l => l.Images)
                .Select(l => new ListingListItemModel
                {
                    ListingId = l.Id,
                    Description = l.Description,
                    ImageUrl = l.Images.OrderBy(i => i.IsFeature).FirstOrDefault().ImageString,
                    Title = l.Title,
                    Year = l.Year,
                    Kilometers = l.Kilometers,
                    Price = l.Price
                })
                .ToList();
        }
        public List<ListingListItemModel> GetInactiveInventoryList()
        {
            return _db.Listings
                .Where(l => !l.IsActive)
                .Include(l => l.Images)
                .Select(l => new ListingListItemModel
                {
                    ListingId = l.Id,
                    Description = l.Description,
                    ImageUrl = l.Images.OrderBy(i => i.IsFeature).FirstOrDefault().ImageString,
                    Title = l.Title,
                    Year = l.Year,
                    Kilometers = l.Kilometers,
                    Price = l.Price
                })
                .ToList();
        }
        
        public async Task<Listing> AddListingAsync()
        {
            return await AddListingAsync(new Listing());
        }

        public async Task<Listing> AddListingAsync(Listing listing)
        {
            var result = await _db.Listings.AddAsync(listing);
            await _db.SaveChangesAsync();
            return result.Entity;
        }

        public async Task DeleteListingAsync(Guid listingId)
        {
            var listing = await _db.Listings.FindAsync(listingId);
            if (listing != null)
            {
                _db.Listings.Remove(listing);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<ListingDetailsImageModel> GetListing(Guid listingId)
        {
            var listing = await _db.Listings.FindAsync(listingId);
            var result = new ListingDetailsImageModel
            {
                ListingId = listing.Id,
                Images = listing.Images?.Select(i => i.ImageString).ToList(),
                Description = listing.Description,
                Title = listing.Title,
                Seats = listing.Seats,
                Doors = listing.Doors,
                FuelType = listing.FuelType,
                BodyType = listing.BodyType,
                Kilometers = listing.Kilometers,
                Year = listing.Year,
                Make = listing.Make,
                Model = listing.Model,
                Transmission = listing.Transmission,
                Colour = listing.Colour,
                DriveTrain = listing.DriveTrain,
                Price = listing.Price
            };

            return result;
        }


        public async Task UpdateListing(ListingDetailsModel viewModel)
        {
            var listing = await _db.Listings.FindAsync(viewModel.ListingId);

            listing.BodyType = viewModel.BodyType;
            listing.Model = viewModel.Model;
            listing.Make = viewModel.Make;
            listing.Kilometers = viewModel.Kilometers;
            listing.Seats = viewModel.Seats;
            listing.Transmission = viewModel.Transmission;
            listing.Year = viewModel.Year;
            listing.DriveTrain = viewModel.DriveTrain;
            listing.Doors = viewModel.Doors;
            listing.Colour = viewModel.Colour;
            listing.Price = viewModel.Price;
            _db.Listings.Update(listing);
            _db.SaveChanges();

            //Update Listing Info
            listing.Description = viewModel.Description;
            listing.Title = viewModel.Title;


            _db.Listings.Update(listing);
            _db.SaveChanges();
        }

    }
}
