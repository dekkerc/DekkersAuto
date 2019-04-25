using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DekkersAuto.Web.Data;
using DekkersAuto.Web.Data.Models;
using DekkersAuto.Web.Models.Inventory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DekkersAuto.Web.Controllers
{
    public class InventoryController : Controller
    {
        private ApplicationDbContext _db;

        public InventoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var model = new InventoryViewModel();

            model.Filter = new FilterViewModel
            {
                ModelList = _db.Models.Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.Name }).ToList(),
                MakeList = _db.Makes.Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.Name }).ToList(),
                ColourList = Util.GetColours()
            };

            model.InventoryList = _db.Listings
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
            

            return View(model);
        }

        /// <summary>
        /// Action to filter the displayed listings by the parameters outlined in the filterviewmodel
        /// </summary>
        /// <param name="model">Model containing parameters to filter on</param>
        /// <returns>Updated partial view with filtered results</returns>
        [HttpPost]
        public IActionResult Filter(FilterViewModel model)
        {
            var inventory = _db.Listings.Include(l => l.Car).Include(l => l.Images).ToList();

            if(model.Colour != "")
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

            return PartialView("_InventoryListPartial", inventory.Select(l => new InventoryListItemViewModel
            {
                Description = l.Description,
                ImageUrl = l.Images.SingleOrDefault(i => i.IsFeature)?.ImageString,
                Kilometers = l.Car.Kilometers,
                Year = l.Car.Year,
                Title = l.Title,
                ListingId = l.Id
            }));
        }
    }
}