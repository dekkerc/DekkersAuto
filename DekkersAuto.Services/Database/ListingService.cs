using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DekkersAuto.Database;
using DekkersAuto.Database.Models;
using DekkersAuto.Services.Models;
using Microsoft.EntityFrameworkCore;
//I, Christopher Dekker, student number 000311337, certify that all code
//submitted is my own work; that I have not copied it from any other source
//I also certify that I have not allowed by work to be copied by others
namespace DekkersAuto.Services.Database
{
    /// <summary>
    /// Service to handle all Listing related actions
    /// </summary>
    public class ListingService : DbServiceBase
    {
        public ListingService(ApplicationDbContext db) : base(db)
        {
        }

        /// <summary>
        /// Retrieves a list of all active listings
        /// </summary>
        /// <returns></returns>
        public List<ListingListItemModel> GetActiveInventoryList()
        {
            return _db.Listings
                .Where(l => l.IsActive)
                .Include(l => l.Images)
                .Select(l => new ListingListItemModel
                {
                    ListingId = l.Id,
                    Description = l.Description,
                    ImageUrl = l.Images.OrderByDescending(i => i.IsFeature).FirstOrDefault().ImageString,
                    Title = l.Title,
                    Year = l.Year,
                    Kilometers = l.Kilometers,
                    Price = l.Price
                })
                .ToList();
        }

        /// <summary>
        /// Retrieves a listing of all inactive listings
        /// </summary>
        /// <returns></returns>
        public List<ListingListItemModel> GetInactiveInventoryList()
        {
            return _db.Listings
                .Where(l => !l.IsActive)
                .Include(l => l.Images)
                .Select(l => new ListingListItemModel
                {
                    ListingId = l.Id,
                    Description = l.Description,
                    ImageUrl = l.Images.OrderByDescending(i => i.IsFeature).FirstOrDefault().ImageString,
                    Title = l.Title,
                    Year = l.Year,
                    Kilometers = l.Kilometers,
                    Price = l.Price
                })
                .ToList();
        }

        /// <summary>
        /// Adds a default, empty listing
        /// </summary>
        /// <returns></returns>
        public async Task<Listing> AddListingAsync()
        {
            return await AddListingAsync(new Listing());
        }

        /// <summary>
        /// Adds a listing to the db
        /// </summary>
        /// <param name="listing"></param>
        /// <returns></returns>
        public async Task<Listing> AddListingAsync(Listing listing)
        {
            var result = await _db.Listings.AddAsync(listing);
            await _db.SaveChangesAsync();
            return result.Entity;
        }

        /// <summary>
        /// Deletes a listing
        /// </summary>
        /// <param name="listingId"></param>
        /// <returns></returns>
        public async Task DeleteListingAsync(Guid listingId)
        {
            var listing = await _db.Listings.FindAsync(listingId);
            if (listing != null)
            {
                _db.Listings.Remove(listing);
                await _db.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Gets a listing by ID
        /// </summary>
        /// <param name="listingId"></param>
        /// <returns></returns>
        public async Task<ListingDetailsImageModel> GetListing(Guid listingId)
        {
            var listing = await _db.Listings
                .Where(l => l.Id == listingId)
                .Select(l => new ListingDetailsImageModel
                {
                    ListingId = l.Id,
                    Images = l.Images.Select(i => new ImageModel
                    {
                        Source = i.ImageString,
                        IsFeature = i.IsFeature,
                        Id = i.Id,
                        ListingId = i.ListingId
                    }).ToList(),
                    Description = l.Description,
                    Title = l.Title,
                    Seats = l.Seats,
                    Doors = l.Doors,
                    FuelType = l.FuelType,
                    BodyType = l.BodyType,
                    Kilometers = l.Kilometers,
                    Year = l.Year,
                    Make = l.Make,
                    Model = l.Model,
                    Transmission = l.Transmission,
                    Colour = l.Colour,
                    DriveTrain = l.DriveTrain,
                    Price = l.Price
                }).SingleOrDefaultAsync();

            return listing;
        }

        /// <summary>
        /// Updates a listing
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
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
            listing.Description = viewModel.Description;
            listing.Title = viewModel.Title;


            _db.Listings.Update(listing);
            await _db.SaveChangesAsync();
        }

        /// <summary>
        /// Activates a listing by ID
        /// </summary>
        /// <param name="listingId"></param>
        /// <returns></returns>
        public async Task ActivateListing(Guid listingId)
        {
            var listing = await _db.Listings.FindAsync(listingId);

            listing.IsActive = true;

            _db.Listings.Update(listing);
            await _db.SaveChangesAsync();
        }

        /// <summary>
        /// Deactivates a listing by ID
        /// </summary>
        /// <param name="listingId"></param>
        /// <returns></returns>
        public async Task DeactivateListing(Guid listingId)
        {
            var listing = await _db.Listings.FindAsync(listingId);

            listing.IsActive = false;

            _db.Listings.Update(listing);
            await _db.SaveChangesAsync();
        }

    }
}
