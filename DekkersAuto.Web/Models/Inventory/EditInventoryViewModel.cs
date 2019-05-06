using DekkersAuto.Web.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DekkersAuto.Web.Models.Inventory
{
    public class EditInventoryViewModel
    {
        public Guid ListingId { get; set; }

        [Required]
        public string Make { get; set; }

        public List<SelectListItem> MakeList { get; set; }
        [Required]
        public string Model { get; set; }
        public List<SelectListItem> ModelList { get; set; }

        public string Transmission { get; set; }
        public List<SelectListItem> TransmissionList { get; set; }
        [Display(Name = "DriveTrain", ResourceType = typeof(Locale))]
        public string DriveTrain { get; set; }
        public string Colour { get; set; }
        public List<SelectListItem> ColourList { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public int Kilometers { get; set; }
        [Display(Name = "BodyType", ResourceType = typeof(Locale))]
        public string BodyType { get; set; }
        [Display(Name = "FuelType", ResourceType = typeof(Locale))]
        public string FuelType { get; set; }
        public int Doors { get; set; }
        public int Seats { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }

        public List<IFormFile> Images { get; set; }

        public List<string> ImageStrings { get; set; }


        public void PopulateListing(Listing listing)
        {
            ListingId = listing.Id;
            ImageStrings = listing.Images.Select(i => i.ImageString).ToList();
            Description = listing.Description;
            Title = listing.Title;
            Seats = listing.Car.Seats;
            Doors = listing.Car.Doors;
            FuelType = listing.Car.FuelType;
            BodyType = listing.Car.BodyType;
            Kilometers = listing.Car.Kilometers;
            Year = listing.Car.Year;
            Make = listing.Car.Make;
            Model = listing.Car.Model;
            Transmission = listing.Car.Transmission;
            Colour = listing.Car.Colour;
            DriveTrain = listing.Car.DriveTrain;
        }
    }
}
