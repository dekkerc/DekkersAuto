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
    /// <summary>
    /// Controller to handle all interactions concerning inventory management and searching
    /// </summary>
    public class InventoryController : Controller
    {
        private DbService _dbService;
        private ListingService _listingService;
        private OptionsService _optionsService;
        private ImageService _imageService;

        /// <summary>
        /// Constructor for Inventor controller containing all required services
        /// </summary>
        /// <param name="service"></param>
        /// <param name="listingService"></param>
        /// <param name="optionsService"></param>
        /// <param name="imageService"></param>
        public InventoryController(DbService service, ListingService listingService, OptionsService optionsService, ImageService imageService)
        {
            _dbService = service;
            _listingService = listingService;
            _optionsService = optionsService;
            _imageService = imageService;
        }

        /// <summary>
        /// Action to seve the inventory list page
        /// </summary>
        /// <returns></returns>
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

            return Redirect("Edit?listingId=" + listing.Id.ToString());
        }

        /// <summary>
        /// Action to display edit page for a listing
        /// </summary>
        /// <param name="listingId">ID of listing to be edited</param>
        /// <returns></returns>
        [Authorize]
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
                Seats = viewModel.Seats,
                Title = viewModel.Title,
                Description = viewModel.Description,
                Price = viewModel.Price
            };

            await _listingService.UpdateListing(listingDetailsModel);

            if (!ModelState.IsValid)
            {
                viewModel.TransmissionList = Util.GetTransmissions();
                viewModel.Options = _optionsService.GetOptions(viewModel.ListingId);
                return View(viewModel);
            }

            await _listingService.ActivateListing(listingDetailsModel.ListingId);

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Action to delete a listing
        /// </summary>
        /// <param name="listingId">ID of listing to be deleted</param>
        /// <returns></returns>
        [Authorize]
        public async Task Delete(Guid listingId)
        {
            await _listingService.DeleteListingAsync(listingId);
        }

        /// <summary>
        /// Action to retrieve the details page for a listing
        /// </summary>
        /// <param name="listingId">ID of listing to be displayed</param>
        /// <returns></returns>
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

        /// <summary>
        /// Action to add or remove an option from a listing
        /// </summary>
        /// <param name="optionId">ID of option to be toggled</param>
        /// <param name="listingId">Listing for option to be add to/removed from</param>
        /// <returns></returns>
        public async Task<IActionResult> UpdateOption(Guid optionId, Guid listingId)
        {
            var model = await _optionsService.UpdateOption(optionId, listingId);

            return PartialView("_Option", model);
        }

        /// <summary>
        /// Action to retrieve listings based on a search parameter
        /// </summary>
        /// <param name="searchTerm">Search parameter</param>
        /// <param name="listingId"></param>
        /// <returns></returns>
        public IActionResult SearchOptions(string searchTerm, Guid listingId)
        {
            var viewModel = _optionsService.SearchOptions(searchTerm, listingId);
            if (viewModel.Any())
            {
                return PartialView("_OptionsList", viewModel);
            }
            return Ok(false);

        }

        /// <summary>
        /// Action to create a new option and add it to a listing
        /// </summary>
        /// <param name="option">Description of the listing to be created</param>
        /// <param name="listingId">ID of listing to add the option to</param>
        /// <returns></returns>
        public async Task<IActionResult> AddOption(string option, Guid listingId)
        {
            var optionItem = await _optionsService.CreateOptionAsync(option);

            if (optionItem != null)
            {
                await _optionsService.UpdateOption(optionItem.Id, listingId);
            }
            var viewModel = _optionsService.SearchOptions(option, listingId);

            return PartialView("_OptionsList", viewModel);
        }

        /// <summary>
        /// Action to add an image to a listing
        /// </summary>
        /// <param name="image">Encoded string representing the image</param>
        /// <param name="listingId">ID of listing to add image to</param>
        /// <returns></returns>
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

        /// <summary>
        /// Action to remove an image from a listing
        /// </summary>
        /// <param name="imageId">ID of image to be removed</param>
        /// <returns></returns>
        public async Task RemoveImage(Guid imageId)
        {
            await _imageService.DeleteImageAsync(imageId);
        }

        /// <summary>
        /// Action to set the feature image of a listing
        /// </summary>
        /// <param name="imageId">ID of image to set as feature</param>
        /// <param name="listingId">Listing of the image</param>
        /// <returns></returns>
        public async Task<IActionResult> SetFeatureImage(Guid imageId, Guid listingId)
        {
            await _imageService.SetFeatureImage(imageId, listingId);

            var images = _imageService.GetListingImages(listingId);

            return PartialView("_ImagesList", images); 
        }
    }
}