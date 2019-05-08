using DekkersAuto.Web.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DekkersAuto.Web.Models.Inventory
{
    public abstract class ListingBase
    {
        public Guid ListingId { get; set; }
        [Required]
        public string Make { get; set; }
        [Required]
        public string Model { get; set; }
        public string Transmission { get; set; }
        [Display(Name = "DriveTrain", ResourceType = typeof(Locale))]
        public string DriveTrain { get; set; }
        public string Colour { get; set; }
        [Required]
        public int? Year { get; set; }
        [Required]
        public int? Kilometers { get; set; }
        [Display(Name = "BodyType", ResourceType = typeof(Locale))]
        public string BodyType { get; set; }
        [Display(Name = "FuelType", ResourceType = typeof(Locale))]
        public string FuelType { get; set; }
        public int? Doors { get; set; }
        public int? Seats { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public List<string> ImageStrings { get; set; }

        public void Populate(Listing listing)
        {
            ListingId = listing.Id;
            ImageStrings = listing.Images?.Select(i => i.ImageString).ToList();
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
        }
    }
}
