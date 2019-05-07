﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DekkersAuto.Web.Data;
using DekkersAuto.Web.Data.Models;
using DekkersAuto.Web.Models.Inventory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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

            var listingId = await _dbService.AddListingAsync(listing);

            await _dbService.AddImagesToListingAsync(listingId, viewModel.Images);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid listingId)
        {
            var listing = await _dbService.GetListing(listingId);
            
            var viewModel = new EditInventoryViewModel
            {
                ColourList = Util.GetColours(),
                MakeList = _dbService.GetMakeList(),
                ModelList = _dbService.GetModelList(),
                TransmissionList = Util.GetTransmissions()
            };
            viewModel.Populate(listing);

            return View(viewModel);
        }

        /// <summary>
        /// Method to update the contents of a listing object
        /// Returns back to the listing index page
        /// </summary>
        /// <param name="viewModel">Edit model containing parameters for updating the listing</param>
        /// <returns>The inventory index</returns>
        [HttpPost]
        public async Task<IActionResult> Edit(EditInventoryViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.ColourList = Util.GetColours();
                viewModel.MakeList = _dbService.GetMakeList();
                viewModel.ModelList = _dbService.GetModelList();
                viewModel.TransmissionList = Util.GetTransmissions();
                return View(viewModel);
            }

            await _dbService.UpdateListing(viewModel);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid listingId)
        {
            var listing = await _dbService.GetListing(listingId);

            var viewModel = new DetailViewModel();
            viewModel.Populate(listing);
            
            return View(viewModel);
        }


        /// <summary>
        /// Action to filter the displayed listings by the parameters outlined in the filterviewmodel
        /// </summary>
        /// <param name="viewModel">Model containing parameters to filter on</param>
        /// <returns>Updated partial view with filtered results</returns>
        [HttpPost]
        public IActionResult Filter(FilterViewModel viewModel)
        {
            var result = _dbService.FilterListings(viewModel);
            
            return PartialView("_InventoryListPartial", result);
        }

    }
}