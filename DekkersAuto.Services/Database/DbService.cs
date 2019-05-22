using DekkersAuto.Database;
using DekkersAuto.Services.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DekkersAuto.Services.Database
{
    public class DbService :DbServiceBase
    {

        public DbService(ApplicationDbContext db) : base(db)
        {
        }

        public List<ListingListItemModel> FilterListings(FilterModel model)
        {
            var inventory = _db.Listings.Include(l => l.Images).ToList();

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
                Description = l.Description,
                ImageUrl = l.Images.SingleOrDefault(i => i.IsFeature)?.ImageString,
                Kilometers = l.Kilometers,
                Year = l.Year,
                Title = l.Title,
                ListingId = l.Id
            }).ToList();
        }

        
    }

}
