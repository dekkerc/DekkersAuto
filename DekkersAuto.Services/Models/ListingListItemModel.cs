using System;
using System.Collections.Generic;
using System.Text;

namespace DekkersAuto.Services.Models
{
    /// <summary>
    /// Model holding information of a listing to display in a list
    /// </summary>
    public class ListingListItemModel
    {
        /// <summary>
        /// Gets and sets the ListingId
        /// </summary>
        public Guid ListingId { get; set; }
        /// <summary>
        /// Gets and sets the Description
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Gets and sets the ImageUrl
        /// </summary>
        public string ImageUrl { get; set; }
        /// <summary>
        /// Gets and sets the Title
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Gets and sets the Year
        /// </summary>
        public int? Year { get; set; }
        /// <summary>
        /// Gets and sets the Kilometers
        /// </summary>
        public int? Kilometers { get; set; }
        /// <summary>
        /// Gets and sets the Price
        /// </summary>
        public double? Price { get; set; }
    }
}
