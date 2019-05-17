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
    public class DbService :DbServiceBase
    {

        public DbService(ApplicationDbContext db) : base(db)
        {
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

        
    }

}
