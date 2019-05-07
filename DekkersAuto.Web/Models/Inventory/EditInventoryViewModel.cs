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
    public class EditInventoryViewModel: ListingBase
    {
        public List<SelectListItem> ModelList { get; set; }

        public List<SelectListItem> MakeList { get; set; }

        public List<SelectListItem> TransmissionList { get; set; }
        
        public List<SelectListItem> ColourList { get; set; }
        
        public List<IFormFile> Images { get; set; }
        
    }
}
