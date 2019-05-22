using System;
using System.Collections.Generic;
using System.Text;

namespace DekkersAuto.Services.Models
{
    public class ImageDetailsModel
    {
        /// <summary>
        /// Gets and sets the Id
        /// </summary>
        public Guid ImageId { get; set; }
        /// <summary>
        /// Gets and sets the ListingId
        /// Represents the listing that the image is for
        /// </summary>
        public Guid ListingId { get; set; }
        /// <summary>
        /// Gets and sets the image string
        /// A base64 encoded image url
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// Gets and sets IsFeature
        /// Boolean representing if the image is a feature image
        /// </summary>
        public bool IsFeature { get; set; }
    }
}
