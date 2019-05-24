using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DekkersAuto.Database;
using DekkersAuto.Database.Models;
using DekkersAuto.Services.Models;

namespace DekkersAuto.Services.Database
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

        public List<SelectedOptionModel> GetOptions(Guid listingId)
        {
            return _db.Options
                .Select(o =>
                new SelectedOptionModel
                {
                    Id = o.Id,
                    Description = o.Description,
                    ListingId = listingId,
                    Selected = o.ListingOptions.Any( l => l.ListingId == listingId)
                })
                .ToList();
        }

        public async Task AddOptionsToListingAsync(Guid carId, List<Guid> selectedOptions)
        {
            await _db.ListingOptions.AddRangeAsync(selectedOptions.Select(o => new ListingOption { ListingId = carId, OptionId = o }));
            await _db.SaveChangesAsync();
        }

        public async Task<SelectedOptionModel> UpdateOption(Guid optionId, Guid listingId)
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
            return new SelectedOptionModel
            {
                ListingId = listingId,
                Id = optionId,
                Description = _db.Options.Find(optionId).Description,
                Selected = isSelected
            };
        }

        public List<SelectedOptionModel> SearchOptions(string searchTerm, Guid listingId)
        {
            return _db.Options
                .Where(o => string.IsNullOrEmpty(searchTerm) || o.Description.ToLowerInvariant().Contains(searchTerm.ToLowerInvariant()))
                .Select(o =>
                    new SelectedOptionModel
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
