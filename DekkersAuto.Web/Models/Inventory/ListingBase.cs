using DekkersAuto.Services.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DekkersAuto.Web.Models.Inventory
{
     /// <summary>
     /// Base class containing the properties required to define a listing
     /// </summary>
    public abstract class ListingBase
    {
        /// <summary>
        /// Gets and sets the ListingId
        /// </summary>
        public Guid ListingId { get; set; }

        /// <summary>
        /// Gets and sets the Make
        /// </summary>
        [Required]
        public string Make { get; set; }

        /// <summary>
        /// Gets and sets the Model
        /// </summary>
        [Required]
        public string Model { get; set; }

        /// <summary>
        /// Gets and sets the Transmission
        /// </summary>
        public string Transmission { get; set; }

        /// <summary>
        /// Gets and sets the DriveTrain
        /// </summary>
        [Display(Name = "DriveTrain", ResourceType = typeof(Locale))]
        public string DriveTrain { get; set; }

        /// <summary>
        /// Gets and sets the Colour
        /// </summary>
        public string Colour { get; set; }

        /// <summary>
        /// Gets and sets the Year
        /// </summary>
        [Required]
        public int? Year { get; set; }

        /// <summary>
        /// Gets and sets the Kilometers
        /// </summary>
        [Required]
        public int? Kilometers { get; set; }

        /// <summary>
        /// Gets and sets the BodyType
        /// </summary>
        [Display(Name = "BodyType", ResourceType = typeof(Locale))]
        public string BodyType { get; set; }

        /// <summary>
        /// Gets and sets the FuelType
        /// </summary>
        [Display(Name = "FuelType", ResourceType = typeof(Locale))]
        public string FuelType { get; set; }

        /// <summary>
        /// Gets and sets the Doors
        /// </summary>
        public int? Doors { get; set; }

        /// <summary>
        /// Gets and sets the Seats
        /// </summary>
        public int? Seats { get; set; }

        /// <summary>
        /// Gets and sets the Title
        /// </summary>
        [Required]
        public string Title { get; set; }

        /// <summary>
        /// Gets and sets the Description
        /// </summary>
        [Required]
        public string Description { get; set; }

        /// <summary>
        /// Gets and sets the Images
        /// </summary>
        public List<string> Images { get; set; }

        /// <summary>
        /// Gets and sets the ImageModels
        /// </summary>
        public List<ImageModel> ImageModels { get; set; }

        /// <summary>
        /// Gets and sets the Price
        /// </summary>
        [Required]
        public double? Price { get; set; }


        /// <summary>
        /// Method to populate the listing model from the service model
        /// </summary>
        public virtual void Populate(ListingDetailsImageModel listing)
        {
            ListingId = listing.ListingId;
            Images = listing.Images.Select(image => image.Source).ToList();
            ImageModels = listing.Images;
            Description = listing.Description;
            Title = listing.Title;
            Seats = listing.Seats;
            Doors = listing.Doors;
            FuelType = listing.FuelType;
            BodyType = listing.BodyType;
            Kilometers = listing.Kilometers;
            Year = listing.Year;
            Make = listing.Make;
            Model = listing.Model;
            Transmission = listing.Transmission;
            Colour = listing.Colour;
            DriveTrain = listing.DriveTrain;
            Price = listing.Price;
        }
    }
}
