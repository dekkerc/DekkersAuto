using DekkersAuto.Web.Data;
using DekkersAuto.Web.Data.Models;
using DekkersAuto.Web.Models.Inventory;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
            return _db.Models.Select(m => new SelectListItem { Text = m.Name }).ToList();
        }

        public List<SelectListItem> GetMakeList()
        {
            return _db.Makes.Select(m => new SelectListItem { Text = m.Name }).ToList();
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
                    ImageUrl = l.Images.SingleOrDefault(i => i.IsFeature).ImageString,
                    Title = l.Title,
                    Year = l.Car.Year,
                    Kilometers = l.Car.Kilometers
                })
                .ToList();
        }

        public async Task AddListingAsync(Listing listing)
        {
            await _db.Listings.AddAsync(listing);
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
        

        public Banner GetBanner()
        {
            var banner = _db.Banners.FirstOrDefault();
            return banner;
        }

        public string GetRole(IdentityUser user)
        {
            return _db.UserRoles.SingleOrDefault(ur => ur.UserId == user.Id)?.RoleId;
        }

        public List<InventoryListItemViewModel> FilterListings(FilterViewModel model)
        {
            var inventory = _db.Listings.Include(l => l.Car).Include(l => l.Images).ToList();

            if (model.Colour != "")
            {
                inventory = inventory.Where(l => l.Car.Colour == model.Colour).ToList();
            }
            if (model.Make.HasValue)
            {
                inventory = inventory.Where(l => l.Car.Make == _db.Makes.Find(model.Make).Name).ToList();
            }
            if (model.Model.HasValue)
            {
                inventory = inventory.Where(l => l.Car.Model == _db.Models.Find(model.Model).Name).ToList();
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
    }

}
