using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DekkersAuto.Database.Models
{
    /// <summary>
    /// Represents an image for a listing
    /// </summary>
    public class Image
    {
        /// <summary>
        /// Gets and sets the Id
        /// </summary>
        [Key]
        public Guid Id { get; set; }
        /// <summary>
        /// Gets and sets the ListingId
        /// Represents the listing that the image is for
        /// </summary>
        [ForeignKey("Listing")]
        public Guid ListingId { get; set; }
        /// <summary>
        /// Gets and sets the image string
        /// A base64 encoded image url
        /// </summary>
        public string ImageString { get; set; }

        /// <summary>
        /// Gets and sets IsFeature
        /// Boolean representing if the image is a feature image
        /// </summary>
        public bool IsFeature { get; set; }
    }
}
