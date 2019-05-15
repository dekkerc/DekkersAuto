using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DekkersAuto.Web.Data;
using DekkersAuto.Web.Data.Models;
using DekkersAuto.Web.Models.Inventory;
using DekkersAuto.Web.Services;
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

                InventoryList = _dbService.GetActiveInventoryList()
            };


            return View(model);
        }

        /// <summary>
        /// Action to display the create page. Only available to signed in users
        /// </summary>
        /// <returns>The create page</returns>
        [HttpGet, Authorize]
        public async Task<IActionResult> Create()
        {
            var listing = await _dbService.AddListingAsync(new Listing());
            
            return Redirect("CreateListing?listingId=" + listing.Id.ToString());
        }

        public async Task<IActionResult> CreateListing(Guid listingId)
        {
            var listing = await _dbService.GetListing(listingId);
            var viewModel = new CreateInventoryViewModel
            {
                ColourList = Util.GetColours(),
                MakeList = _dbService.GetMakeList(),
                ModelList = _dbService.GetModelList(),
                TransmissionList = Util.GetTransmissions(),
                Options = _dbService.GetOptions(listingId)
            };

            viewModel.Populate(listing);

            return View("Create", viewModel);
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
                viewModel.ColourList = Util.GetColours();
                viewModel.MakeList = _dbService.GetMakeList();
                viewModel.ModelList = _dbService.GetModelList();
                viewModel.TransmissionList = Util.GetTransmissions();
                viewModel.Options = _dbService.GetOptions();
                return View(viewModel);
            }

            await _dbService.UpdateListing(viewModel);
            
            return RedirectToAction("Index");
        }

        public async Task Delete(Guid listingId)
        {
            await _dbService.DeleteListingAsync(listingId);
        }
        
        [HttpGet]
        public async Task<IActionResult> Details(Guid listingId)
        {
            var listing = await _dbService.GetListing(listingId);

            var viewModel = new DetailViewModel();
            viewModel.Populate(listing);
            viewModel.Options = _dbService.GetListingOptions(listing.Id);
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

        public async Task<IActionResult> UpdateOption(Guid optionId, Guid listingId)
        {
            var model = await _dbService.UpdateOption(optionId, listingId);

            return PartialView("_Option", model);
        }

        public IActionResult SearchOptions(string searchTerm, Guid listingId)
        {
            var viewModel = _dbService.SearchOptions(searchTerm, listingId);

            return PartialView("_OptionsList", viewModel);
        }

        public async Task<IActionResult> AddImage(string image, Guid listingId)
        {
            var listingImage = await _dbService.AddImageToListingAsync(listingId, image);

            return PartialView("_Image", new ImageModel
            {
                Source = listingImage.ImageString,
                IsFeature = listingImage.IsFeature,
                ListingId = listingId,
                Id = listingImage.Id
            });
        }
        public async Task RemoveImage(Guid imageId)
        {
            await _dbService.DeleteImageAsync(imageId);
        }

        public async Task<IActionResult> SetFeatureImage(Guid imageId, Guid listingId)
        {
            await _dbService.SetFeatureImage(imageId, listingId);

            var images = _dbService.GetListingImages(listingId);

            return PartialView("_ImagesList", images);

        }

    }
}