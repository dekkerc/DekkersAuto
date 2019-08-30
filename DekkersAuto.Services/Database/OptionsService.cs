using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DekkersAuto.Database;
using DekkersAuto.Database.Models;
using DekkersAuto.Services.Models;

namespace DekkersAuto.Services.Database
{
    /// <summary>
    /// Service handling all actions related to options
    /// </summary>
    public class OptionsService : DbServiceBase
    {
        public OptionsService(ApplicationDbContext db) : base(db)
        {
        }

        /// <summary>
        /// Retrieves a list of all options
        /// </summary>
        /// <returns></returns>
        public List<OptionModel> GetOptions()
        {
            return _db.Options.Select(o => new OptionModel { Id = o.Id, Description = o.Description }).ToList();
        }

        /// <summary>
        /// Gets all options
        /// </summary>
        /// <param name="listingId"></param>
        /// <returns></returns>
        public List<SelectedOptionModel> GetOptions(Guid listingId)
        {
            return _db.Options
                .Select(o =>
                new SelectedOptionModel
                {
                    Id = o.Id,
                    Description = o.Description,
                    ListingId = listingId,
                    Selected = o.ListingOptions.Any(l => l.ListingId == listingId)
                })
                .ToList();
        }

        /// <summary>
        /// Adds a list of options to a listing
        /// </summary>
        /// <param name="carId"></param>
        /// <param name="selectedOptions"></param>
        /// <returns></returns>
        public async Task AddOptionsToListingAsync(Guid carId, List<Guid> selectedOptions)
        {
            await _db.ListingOptions.AddRangeAsync(selectedOptions.Select(o => new ListingOption { ListingId = carId, OptionId = o }));
            await _db.SaveChangesAsync();
        }

        /// <summary>
        /// Toggles whether or not an option is applied to a listing
        /// </summary>
        /// <param name="optionId"></param>
        /// <param name="listingId"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Performs a search of listings based on a passed in search parameter
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <param name="listingId"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Gets all options applied to a listing
        /// </summary>
        /// <param name="listingId"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Method to create a new option
        /// </summary>
        /// <param name="option"></param>
        /// <returns></returns>
        public async Task<Option> CreateOptionAsync(string option)
        {
            var newOption = await _db.Options.AddAsync(new Option { Description = option });
            await _db.SaveChangesAsync();
            return newOption.Entity;
        }
    }
}
