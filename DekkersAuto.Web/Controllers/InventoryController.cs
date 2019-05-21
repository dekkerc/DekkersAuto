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
        private ListingService _listingService;
        private OptionsService _optionsService;
        private ImageService _imageService;



        public InventoryController(DbService service, ListingService listingService, OptionsService optionsService, ImageService imageService)
        {
            _dbService = service;
            _listingService = listingService;
            _optionsService = optionsService;
            _imageService = imageService;
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

                InventoryList = _listingService.GetActiveInventoryList()
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
            var listing = await _listingService.AddListingAsync(new Listing());
            
            return Redirect("CreateListing?listingId=" + listing.Id.ToString());
        }

        public async Task<IActionResult> CreateListing(Guid listingId)
        {
            var listing = await _listingService.GetListing(listingId);
            var viewModel = new CreateInventoryViewModel
            {
                ColourList = Util.GetColours(),
                MakeList = _dbService.GetMakeList(),
                ModelList = _dbService.GetModelList(),
                TransmissionList = Util.GetTransmissions(),
                Options = _optionsService.GetOptions(listingId)
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
                viewModel.Options = _optionsService.GetOptions();
                return View(viewModel);
            }

            await _listingService.UpdateListing(viewModel);
            
            return RedirectToAction("Index");
        }

        public async Task Delete(Guid listingId)
        {
            await _listingService.DeleteListingAsync(listingId);
        }
        
        [HttpGet]
        public async Task<IActionResult> Details(Guid listingId)
        {
            var listing = await _listingService.GetListing(listingId);

            var viewModel = new DetailViewModel();
            viewModel.Populate(listing);
            viewModel.Options = _optionsService.GetListingOptions(listing.Id);
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
            var model = await _optionsService.UpdateOption(optionId, listingId);

            return PartialView("_Option", model);
        }

        public IActionResult SearchOptions(string searchTerm, Guid listingId)
        {
            var viewModel = _optionsService.SearchOptions(searchTerm, listingId);

            return PartialView("_OptionsList", viewModel);
        }

        public async Task<IActionResult> AddImage(string image, Guid listingId)
        {
            var listingImage = await _imageService.AddImageToListingAsync(listingId, image);

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
            await _imageService.DeleteImageAsync(imageId);
        }

        public async Task<IActionResult> SetFeatureImage(Guid imageId, Guid listingId)
        {
            await _imageService.SetFeatureImage(imageId, listingId);

            var images = _imageService.GetListingImages(listingId);

            return PartialView("_ImagesList", images);

        }

    }
}