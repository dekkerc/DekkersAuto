using DekkersAuto.Services.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DekkersAuto.Web.Models.Inventory
{
    public class CreateInventoryViewModel: ListingBase
    {
        public List<SelectListItem> TransmissionList { get; set; }
        public List<Guid> SelectedOptions { get; set; }
        public List<SelectedOptionModel> Options { get; set; }
    }
}
