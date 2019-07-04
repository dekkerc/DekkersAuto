using System;
using System.Linq;
using System.Threading.Tasks;
using DekkersAuto.Services.Database;
using DekkersAuto.Services.Models;
using DekkersAuto.Services.Services;
using DekkersAuto.Web.Models.Inventory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DekkersAuto.Web.Controllers
{
    public class InventoryController : Controller
    {
        private DbService _dbService;
        private ListingService _listingService;
        private OptionsService _optionsService;
        private ImageService _imageService;
        private ApiService _apiService;


        public InventoryController(DbService service, ListingService listingService, OptionsService optionsService, ImageService imageService, ApiService apiService)
        {
            _dbService = service;
            _listingService = listingService;
            _optionsService = optionsService;
            _imageService = imageService;
            _apiService = apiService;
        }


        public async Task<IActionResult> Index()
        {
            var model = new InventoryViewModel
            {
                Filter = new FilterViewModel(),
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
            var listing = await _listingService.AddListingAsync();
            
            return Redirect("edit?listingId=" + listing.Id.ToString());
        }

        public async Task<IActionResult> Edit(Guid listingId)
        {
            var listing = await _listingService.GetListing(listingId);

            var viewModel = new CreateInventoryViewModel
            {
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
                viewModel.TransmissionList = Util.GetTransmissions();
                viewModel.Options = _optionsService.GetOptions(viewModel.ListingId);
                return View(viewModel);
            }

            var listingDetailsModel = new ListingDetailsModel
            {
                ListingId = viewModel.ListingId,
                Make = viewModel.Make,
                Model = viewModel.Model,
                Transmission = viewModel.Transmission,
                DriveTrain = viewModel.DriveTrain,
                Colour = viewModel.Colour,
                Year = viewModel.Year,
                Kilometers = viewModel.Kilometers,
                BodyType = viewModel.BodyType,
                FuelType = viewModel.FuelType,
                Doors = viewModel.Doors,
                Seats =viewModel.Seats,
                Title = viewModel.Title,
                Description =viewModel.Description,
                Price = viewModel.Price
            };

            await _listingService.UpdateListing(listingDetailsModel);
            
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
            viewModel.Options = _optionsService.GetListingOptions(listing.ListingId);
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