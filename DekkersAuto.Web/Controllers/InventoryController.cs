using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DekkersAuto.Web.Data;
using DekkersAuto.Web.Data.Models;
using DekkersAuto.Web.Models.Inventory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DekkersAuto.Web.Controllers
{
    public class InventoryController : Controller
    {
        private DbService _dbService;

        public InventoryController(DbService service)
        {
            _dbService = service;
        }

        public IActionResult Index()
        {
            var model = new InventoryViewModel
            {
                Filter = new FilterViewModel
                {
                    ModelList = _dbService.GetModelList(),
                    MakeList = _dbService.GetMakeList(),
                    ColourList = Util.GetColours()
                },

                InventoryList = _dbService.GetInventoryList()
            };


            return View(model);
        }

        /// <summary>
        /// Action to display the create page. Only available to signed in users
        /// </summary>
        /// <returns>The create page</returns>
        [HttpGet, Authorize]
        public IActionResult Create()
        {
            var viewModel = new CreateInventoryViewModel
            {
                ColourList = Util.GetColours(),
                MakeList = _dbService.GetMakeList(),
                ModelList = _dbService.GetModelList(),
                TransmissionList = Util.GetTransmissions()
            };

            return View(viewModel);
        }

        /// <summary>
        /// Action to create a new inventory item.
        /// </summary>
        /// <param name="model">Inventory model used to create new listing</param>
        /// <returns>Redirects to the inventory view</returns>
        [HttpPost, Authorize]
        public async Task<IActionResult> Create(CreateInventoryViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var listing = new Listing
            {
                Title = viewModel.Title,
                Description = viewModel.Description,
                Car = new Car
                {
                    Make = viewModel.Make,
                    Model = viewModel.Model,
                    BodyType = viewModel.BodyType,
                    Colour = viewModel.Colour,
                    FuelType = viewModel.FuelType,
                    Doors = viewModel.Doors,
                    Seats = viewModel.Seats,
                    Kilometers = viewModel.Kilometers,
                    Transmission = viewModel.Transmission,
                    Year = viewModel.Year
                }
            };

            await _dbService.AddListingAsync(listing);
            
            return RedirectToAction("Index");
        }


        /// <summary>
        /// Action to filter the displayed listings by the parameters outlined in the filterviewmodel
        /// </summary>
        /// <param name="model">Model containing parameters to filter on</param>
        /// <returns>Updated partial view with filtered results</returns>
        [HttpPost]
        public IActionResult Filter(FilterViewModel model)
        {
            var result = _dbService.FilterListings(model);
            
            return PartialView("_InventoryListPartial", result);
        }
    }
}