using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DekkersAuto.Web.Data;
using DekkersAuto.Web.Data.Models;
using DekkersAuto.Web.Models.Inventory;

namespace DekkersAuto.Web.Services
{
    public class OptionsService : DbServiceBase
    {
        public OptionsService(ApplicationDbContext db) : base(db)
        {
        }

        public List<OptionModel> GetOptions()
        {
            return _db.Options.Select(o => new OptionModel { Id = o.Id, Description = o.Description }).ToList();
        }

        public List<OptionModel> GetOptions(Guid listingId)
        {
            return _db.Options
                .Select(o =>
                new OptionModel
                {
                    Id = o.Id,
                    Description = o.Description,
                    ListingId = listingId,
                    Selected = _db.ListingOptions
                                    .Any(lo => lo.ListingId == listingId && lo.OptionId == o.Id)
                })
                .ToList();
        }

        public async Task AddOptionsToListingAsync(Guid carId, List<Guid> selectedOptions)
        {
            await _db.ListingOptions.AddRangeAsync(selectedOptions.Select(o => new ListingOption { ListingId = carId, OptionId = o }));
            await _db.SaveChangesAsync();
        }

        public async Task<OptionModel> UpdateOption(Guid optionId, Guid listingId)
        {
            var option = _db.ListingOptions.SingleOrDefault(o => o.OptionId == optionId && o.ListingId == listingId);
            bool isSelected;
            if (option == null)
            {
                _db.ListingOptions.Add(new ListingOption
                {
                    ListingId = listingId,
                    OptionId = optionId
                });
                isSelected = true;
            }
            else
            {
                _db.ListingOptions.Remove(option);
                isSelected = false;
            }
            await _db.SaveChangesAsync();
            return new OptionModel
            {
                ListingId = listingId,
                Id = optionId,
                Description = _db.Options.Find(optionId).Description,
                Selected = isSelected
            };
        }

        public List<OptionModel> SearchOptions(string searchTerm, Guid listingId)
        {
            return _db.Options
                .Where(o => string.IsNullOrEmpty(searchTerm) || o.Description.ToLowerInvariant().Contains(searchTerm.ToLowerInvariant()))
                .Select(o =>
                    new OptionModel
                    {
                        Description = o.Description,
                        Id = o.Id,
                        ListingId = listingId,
                        Selected = _db.ListingOptions
                                        .Where(lo => lo.ListingId == listingId && lo.OptionId == o.Id)
                                        .Any()
                    })
                .ToList();
        }


        public List<string> GetListingOptions(Guid listingId)
        {
            return _db.ListingOptions
                .Where(c => c.ListingId == listingId)
                .Join(
                    _db.Options,
                    listingOption => listingOption.OptionId,
                    option => option.Id,
                    (listingOption, option) => option.Description
                )
                .ToList();
        }



    }
}
