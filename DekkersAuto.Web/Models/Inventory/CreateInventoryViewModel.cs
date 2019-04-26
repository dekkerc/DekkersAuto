using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DekkersAuto.Web.Models.Inventory
{
    public class CreateInventoryViewModel
    {
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
        
    }
}
