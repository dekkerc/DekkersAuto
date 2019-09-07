using DekkersAuto.Database;
using DekkersAuto.Services.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
//I, Christopher Dekker, student number 000311337, certify that all code
//submitted is my own work; that I have not copied it from any other source
//I also certify that I have not allowed by work to be copied by others
namespace DekkersAuto.Services.Database
{
    /// <summary>
    /// Service for handling general database interactions
    /// </summary>
    public class DbService :DbServiceBase
    {

        public DbService(ApplicationDbContext db) : base(db)
        {
        }

        /// <summary>
        /// Filters the active listings based on the parameters passed in through the model
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<ListingListItemModel> FilterListings(FilterModel model)
        {
            var inventory = _db.Listings.Include(l => l.Images).Where(l => l.IsActive).ToList();

            if (model.Colour != null)
            {
                inventory = inventory.Where(l => l.Colour == model.Colour).ToList();
            }
            if (model.Make != null)
            {
                inventory = inventory.Where(l => l.Make == model.Make).ToList();
            }
            if (model.Model != null)
            {
                inventory = inventory.Where(l => l.Model == model.Model).ToList();
            }
            if (model.KilometersFrom.HasValue && model.KilometersFrom > 0)
            {
                inventory = inventory.Where(l => l.Kilometers >= model.KilometersFrom).ToList();
            }
            if (model.KilometersTo.HasValue && model.KilometersTo > 0)
            {
                inventory = inventory.Where(l => l.Kilometers <= model.KilometersTo).ToList();
            }
            if (model.YearFrom.HasValue && model.YearFrom > 0)
            {
                inventory = inventory.Where(l => l.Year >= model.YearFrom).ToList();
            }
            if (model.YearTo.HasValue && model.YearTo > 0)
            {
                inventory = inventory.Where(l => l.Year <= model.YearTo).ToList();
            }
            return inventory.Select(l => new ListingListItemModel
            {
                ListingId = l.Id,
                Description = l.Description,
                ImageUrl = l.Images.OrderByDescending(i => i.IsFeature).FirstOrDefault().ImageString,
                Title = l.Title,
                Year = l.Year,
                Kilometers = l.Kilometers,
                Price = l.Price
            }).ToList();
        }

        
    }

}
