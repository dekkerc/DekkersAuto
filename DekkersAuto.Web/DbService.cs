﻿using DekkersAuto.Web.Data;
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

namespace DekkersAuto.Web
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

        public List<InventoryListItemViewModel> GetInventoryList()
        {
            return _db.Listings
                .Include(l => l.Car)
                .Include(l => l.Images)
                .Select(l => new InventoryListItemViewModel
                {
                    ListingId = l.Id,
                    Description = l.Description,
                    ImageUrl = l.Images.OrderBy(i =>i.IsFeature).FirstOrDefault().ImageString,
                    Title = l.Title,
                    Year = l.Car.Year,
                    Kilometers = l.Car.Kilometers
                })
                .ToList();
        }

        public async Task<Guid> AddListingAsync(Listing listing)
        {
            var result = await _db.Listings.AddAsync(listing);
            await _db.SaveChangesAsync();
            return result.Entity.Id;
        }

        public List<SelectListItem> GetRoles()
        {
            var roles = new List<SelectListItem>();
            RoleManager.Roles.ToList().ForEach(r =>
            {
                roles.Add(new SelectListItem
                {
                    Text = r.Name,
                    Value = r.Id
                });
            });
            return roles;
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

        public async Task<Listing> GetListing(Guid listingId)
        {
            var listing = await _db.Listings.Include(l => l.Car).FirstOrDefaultAsync(l => l.Id == listingId);

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

            //Update Car info
            var car = await _db.Cars.FindAsync(listing.CarId);
            car.BodyType = viewModel.BodyType;
            car.Model = viewModel.Model;
            car.Make = viewModel.Make;
            car.Kilometers = viewModel.Kilometers;
            car.Seats = viewModel.Seats;
            car.Transmission = viewModel.Transmission;
            car.Year = viewModel.Year;
            car.DriveTrain = viewModel.DriveTrain;
            car.Doors = viewModel.Doors;
            car.Colour = viewModel.Colour;
            _db.Cars.Update(car);
            _db.SaveChanges();

            //Update Listing Info
            listing.Description = viewModel.Description;
            listing.Title = viewModel.Title;

            if(viewModel.Images != null && viewModel.Images.Count > 0)
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

        public async Task<bool> CreateUserAsync(string username, string password, string roleId)
        {
            var user = new IdentityUser
            {
                UserName = username
            };
            var result = await UserManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                var newUser = await UserManager.FindByNameAsync(user.UserName);
                var role = _db.Roles.Find(roleId);
                var roleResult = await UserManager.AddToRoleAsync(newUser, await RoleManager.GetRoleNameAsync(role));
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

        public string GetRole(IdentityUser user)
        {
            return _db.UserRoles.SingleOrDefault(ur => ur.UserId == user.Id)?.RoleId;
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
            var inventory = _db.Listings.Include(l => l.Car).Include(l => l.Images).ToList();

            if (model.Colour != null)
            {
                inventory = inventory.Where(l => l.Car.Colour == model.Colour).ToList();
            }
            if (model.Make != null)
            {
                inventory = inventory.Where(l => l.Car.Make == model.Make).ToList();
            }
            if (model.Model != null)
            {
                inventory = inventory.Where(l => l.Car.Model == model.Model).ToList();
            }
            if (model.KilometersFrom.HasValue && model.KilometersFrom > 0)
            {
                inventory = inventory.Where(l => l.Car.Kilometers >= model.KilometersFrom).ToList();
            }
            if (model.KilometersTo.HasValue && model.KilometersTo > 0)
            {
                inventory = inventory.Where(l => l.Car.Kilometers <= model.KilometersTo).ToList();
            }
            if (model.YearFrom.HasValue && model.YearFrom > 0)
            {
                inventory = inventory.Where(l => l.Car.Year >= model.YearFrom).ToList();
            }
            if (model.YearTo.HasValue && model.YearTo > 0)
            {
                inventory = inventory.Where(l => l.Car.Year <= model.YearTo).ToList();
            }
            return inventory.Select(l => new InventoryListItemViewModel
            {
                Description = l.Description,
                ImageUrl = l.Images.SingleOrDefault(i => i.IsFeature)?.ImageString,
                Kilometers = l.Car.Kilometers,
                Year = l.Car.Year,
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

            if (model.Role != roles.First())
            {
                await UserManager.RemoveFromRoleAsync(user, roles.First());
                await UserManager.AddToRoleAsync(user, model.Role);
            }
        }

        public async Task DeleteUserAsync(string userId)
        {
            await UserManager.DeleteAsync(await UserManager.FindByIdAsync(userId));
        }
    }

}
