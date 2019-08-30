using System;
using System.Collections.Generic;
using System.Text;

namespace DekkersAuto.Services.Models
{
    /// <summary>
    /// Model representing Listing details including images
    /// </summary>
    public class ListingDetailsImageModel : ListingDetailsModel
    {
        /// <summary>
        /// Gets and sets images
        /// </summary>
        public List<ImageModel> Images { get; set; }
    }
}
