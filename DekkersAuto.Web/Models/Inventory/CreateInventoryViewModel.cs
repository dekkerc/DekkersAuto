using DekkersAuto.Services.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace DekkersAuto.Web.Models.Inventory
{
    /// <summary>
    /// Model holding parameters requried to create a listing
    /// </summary>
    public class CreateInventoryViewModel: ListingBase
    {
        /// <summary>
        /// Gets and sets the Transmission List
        /// </summary>
        public List<SelectListItem> TransmissionList { get; set; }
        /// <summary>
        /// Gets and Sets the Selected Options
        /// </summary>
        public List<Guid> SelectedOptions { get; set; }
        /// <summary>
        /// Gets and sets the options
        /// </summary>
        public List<SelectedOptionModel> Options { get; set; }
    }
}
