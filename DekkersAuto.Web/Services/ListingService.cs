using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DekkersAuto.Web.Data;
using DekkersAuto.Web.Data.Models;
using DekkersAuto.Web.Models.Inventory;
using Microsoft.EntityFrameworkCore;

namespace DekkersAuto.Web.Services
{
    public class ListingService : DbServiceBase
    {
        public ListingService(ApplicationDbContext db) : base(db)
        {
        }
        
        public List<InventoryListItemViewModel> GetActiveInventoryList()
        {
            return _db.Listings
                .Where(l => l.IsActive)
                .Include(l => l.Images)
                .Select(l => new InventoryListItemViewModel
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
        public List<InventoryListItemViewModel> GetInactiveInventoryList()
        {
            return _db.Listings
                .Where(l => !l.IsActive)
                .Include(l => l.Images)
                .Select(l => new InventoryListItemViewModel
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

        public async Task<Listing> GetListing(Guid listingId)
        {
            var listing = await _db.Listings.FindAsync(listingId);

            return listing;
        }


        public async Task UpdateListing(ListingBase viewModel)
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
