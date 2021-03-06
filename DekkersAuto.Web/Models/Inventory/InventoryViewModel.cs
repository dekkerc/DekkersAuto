﻿using DekkersAuto.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DekkersAuto.Web.Models.Inventory
{
    /// <summary>
    /// Class holding display properties for the inventory list
    /// </summary>
    public class InventoryViewModel
    {
        /// <summary>
        /// Gets and sets the filter
        /// </summary>
        public FilterViewModel Filter { get; set; }
        /// <summary>
        /// Gets and sets the InventoryList
        /// Represents the list of individual listings
        /// </summary>
        public List<ListingListItemModel> InventoryList { get; set; }
    }
}
