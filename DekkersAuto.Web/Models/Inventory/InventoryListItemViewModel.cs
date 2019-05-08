using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DekkersAuto.Web.Models.Inventory
{
    /// <summary>
    /// Class holding data for display of summary of each vehicle in inventory
    /// </summary>
    public class InventoryListItemViewModel
    {
        /// <summary>
        /// Gets and sets the ID of the listing
        /// Represents the unique identifier of the listing
        /// </summary>
        public Guid ListingId { get; set; }
        /// <summary>
        /// Gets and sets the ImageUrl
        /// Represents the base64 encoded image url
        /// </summary>
        public string ImageUrl { get; set; }
        /// <summary>
        /// Gets and sets the Title
        /// Represents the title of the listing
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Gets and sets the Year
        /// Represents the year of the listed vehicle
        /// </summary>
        public int? Year { get; set; }
        /// <summary>
        /// Gets and sets the Kilometers
        /// Represents the kilometers of the listed vehicle
        /// </summary>
        public int? Kilometers { get; set; }
        /// <summary>
        /// Gets and sets the Description
        /// Represents the description of the listing
        /// </summary>
        public string Description { get; set; }

    }
}
