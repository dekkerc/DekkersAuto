using DekkersAuto.Web.Data;
using DekkersAuto.Web.Data.Models;
using DekkersAuto.Web.Models;
using DekkersAuto.Web.Models.Account;
using DekkersAuto.Web.Models.Inventory;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DekkersAuto.Web.Services
{
    public class DbService
    {

        /// <summary>
        /// Gets and sets the UserManager
        /// Usermanager handles creation and manipulation of users
        /// </summary>
        private UserManager<IdentityUser> UserManager { get; set; }

        private ApplicationDbContext _db;
        /// <summary>
        /// Gets and sets the RoleManager
        /// Rolemanager handles creation, manipulation and assignment of roles
        /// </summary>
        private RoleManager<IdentityRole> RoleManager { get; set; }

        public DbService(ApplicationDbContext db, RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _db = db;
            RoleManager = roleManager;
            UserManager = userManager;
        }

        public List<SelectListItem> GetModelList()
        {
            var modelList = new List<SelectListItem> { new SelectListItem { Text = "--Select Model--", Value = "" } };
            modelList.AddRange(_db.Models.Select(m => new SelectListItem { Text = m.Name }));
            return modelList;
        }

        public List<SelectListItem> GetMakeList()
        {
            var makeList = new List<SelectListItem> { new SelectListItem { Text = "--Select Make--", Value = "" } };
            makeList.AddRange(_db.Makes.Select(m => new SelectListItem { Text = m.Name }));
            return makeList;
        }

        public List<OptionModel> GetOptions()
        {
            return _db.Options.Select(o => new OptionModel { Id = o.Id, Description = o.Description }).ToList();
        }

        public List<InventoryListItemViewModel> GetInventoryList()
        {
            return _db.Listings
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

        public List<SelectListItem> GetRoles()
        {
            var roles = new List<SelectListItem>();
            RoleManager.Roles.ToList().ForEach(r =>
            {
                roles.Add(new SelectListItem
                {
                    Text = r.Name,
                    Value = r.Name
                });
            });
            return roles;
        }

        public async Task AddOptionsToListingAsync(Guid carId, List<Guid> selectedOptions)
        {
            await _db.ListingOptions.AddRangeAsync(selectedOptions.Select(o => new ListingOption { ListingId = carId, OptionId = o }));
            await _db.SaveChangesAsync();
        }

        public async Task AddImagesToListingAsync(Guid listingId, List<IFormFile> images)
        {
            foreach (var image in images)
            {
                using (var stream = new MemoryStream())
                {
                    await image.CopyToAsync(stream);
                    var listingImage = new Image
                    {
                        ImageString = "data:image/png;base64," + Convert.ToBase64String(stream.ToArray()),
                        ListingId = listingId
                    };
                    await _db.Images.AddAsync(listingImage);
                }
            }

            await _db.SaveChangesAsync();

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
            var listing = await _db.Listings.FirstOrDefaultAsync(l => l.Id == listingId);

            listing.Images = GetListingImages(listingId);
            return listing;
        }


        public IEnumerable<Image> GetListingImages(Guid listingId)
        {
            return _db.Images.Where(i => i.ListingId == listingId).ToList();
        }

        public async Task UpdateListing(EditInventoryViewModel viewModel)
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

            if (viewModel.Images != null && viewModel.Images.Count > 0)
            {
                var images = _db.Images.Where(i => i.ListingId == viewModel.ListingId).ToList();
                _db.Images.RemoveRange(images);
                foreach (var image in viewModel.Images)
                {
                    using (var stream = new MemoryStream())
                    {
                        await image.CopyToAsync(stream);
                        var listingImage = new Image
                        {
                            ImageString = "data:image/png;base64," + Convert.ToBase64String(stream.ToArray()),
                            ListingId = viewModel.ListingId
                        };
                        await _db.Images.AddAsync(listingImage);
                    }
                }

            }

            _db.Listings.Update(listing);
            _db.SaveChanges();
        }

        public List<OptionModel> SearchOptions(string searchTerm)
        {
            return _db.Options.Where(o => string.IsNullOrEmpty(searchTerm) || o.Description.ToLowerInvariant().Contains(searchTerm.ToLowerInvariant())).Select(o => new OptionModel { Description = o.Description, Id = o.Id }).ToList();
        }

        public List<string> GetListingOptions(Guid carId)
        {
            return _db.ListingOptions
                .Where(c => c.ListingId == carId)
                .Join(
                    _db.Options,
                    listingOption => listingOption.OptionId,
                    option => option.Id,
                    (listingOption, option) => option.Description
                )
                .ToList();
        }

        public async Task<bool> CreateUserAsync(string username, string password, string role)
        {
            var user = new IdentityUser
            {
                UserName = username
            };
            var result = await UserManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                var newUser = await UserManager.FindByNameAsync(user.UserName);
                var roleResult = await UserManager.AddToRoleAsync(newUser, role);
            }
            return result.Succeeded;
        }

        public AccountListViewModel GetAccountList(string id)
        {
            var accountList = _db.UserRoles.Join(
                _db.Users,
                userRole => userRole.UserId,
                user => user.Id,
                (userRole, user) => new
                {
                    Id = user.Id,
                    Username = user.UserName,
                    RoleId = userRole.RoleId
                }).Join(
                _db.Roles,
                user => user.RoleId,
                role => role.Id,
                (user, role) => new
                {
                    Id = user.Id,
                    Username = user.Username,
                    Role = role.Name
                })
                .Where(u => u.Id != id)
                .Select(u => new AccountItemViewModel { AccountId = u.Id, Username = u.Username, Role = u.Role })
                .ToList();

            return new AccountListViewModel
            {
                UserId = id,
                Accounts = accountList
            };
        }



        public Banner GetBanner()
        {
            var banner = _db.Banners.FirstOrDefault();
            return banner;
        }
        public void CreateBanner(BannerViewModel model)
        {
            _db.Banners.Add(new Banner { Text = model.Text, IsActive = true });
            _db.SaveChanges();
        }
        public void UpdateBanner(BannerViewModel model)
        {
            var banner = _db.Banners.Find(model.BannerId);

            banner.Text = model.Text;
            banner.IsActive = model.IsActive;
            _db.Banners.Update(banner);
            _db.SaveChanges();
        }

        public async Task<IdentityUser> GetUser(Guid accountId)
        {
            return await UserManager.FindByIdAsync(accountId.ToString());
        }

        public string GetRole(IdentityUser user)
        {
            return _db.UserRoles.SingleOrDefault(ur => ur.UserId == user.Id)?.RoleId;
        }

        public void SeedOptions()
        {
            _db.Options.AddRange(new List<Option>
            {
                new Option{ Description = "Sunroof" },
                new Option{ Description = "Power Seats" },
                new Option{ Description = "Heated Seats" },
                new Option{ Description = "Power Mirrors" },
                new Option{ Description = "Cruise Control" },
                new Option{ Description = "Power Windows" },
            });
            _db.SaveChanges();
        }

        public void SeedMakes()
        {

            _db.Models.AddRange(new List<Model>
            {
                new Model{ Name = "Focus", Make = _db.Makes.Single(m => m.Name == "Ford")},
                new Model{ Name = "Cobalt", Make = _db.Makes.Single(m => m.Name == "Chevrolet")},
                new Model{ Name = "Rio", Make = _db.Makes.Single(m => m.Name == "Kia")},
                new Model{ Name = "Matrix", Make = _db.Makes.Single(m => m.Name == "Toyota")}
            });
            _db.SaveChanges();
        }

        public List<InventoryListItemViewModel> FilterListings(FilterViewModel model)
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
            return inventory.Select(l => new InventoryListItemViewModel
            {
                Description = l.Description,
                ImageUrl = l.Images.SingleOrDefault(i => i.IsFeature)?.ImageString,
                Kilometers = l.Kilometers,
                Year = l.Year,
                Title = l.Title,
                ListingId = l.Id
            }).ToList();
        }

        public async Task UpdateUser(ManageAccountViewModel model)
        {
            var user = await UserManager.FindByIdAsync(model.UserId);

            user.UserName = model.Username;
            await UserManager.UpdateAsync(user);
            var roles = await UserManager.GetRolesAsync(user);

            if (roles.Count == 0)
            {
                await UserManager.AddToRoleAsync(user, model.Role);
            }
            else if (roles.FirstOrDefault() != model.Role)
            {
                await UserManager.RemoveFromRoleAsync(user, roles.FirstOrDefault());
                await UserManager.AddToRoleAsync(user, model.Role);
            }
            _db.SaveChanges();
        }

        public async Task DeleteUserAsync(string userId)
        {
            await UserManager.DeleteAsync(await UserManager.FindByIdAsync(userId));
        }
    }

}
