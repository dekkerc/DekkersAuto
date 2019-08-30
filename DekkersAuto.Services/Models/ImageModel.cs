using System;

namespace DekkersAuto.Services.Models
{
    /// <summary>
    /// Model representing an image
    /// </summary>
    public class ImageModel
    {
        /// <summary>
        /// Gets and sets the Id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Gets and sets the Source
        /// Encoded string representing the image
        /// </summary>
        public string Source { get; set; }
        /// <summary>
        /// Gets and sets the ListingId
        /// </summary>
        public Guid ListingId { get; set; }
        /// <summary>
        /// Gets and sets IsFeature
        /// </summary>
        public bool IsFeature { get; set; }
    }
}
