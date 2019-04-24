using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DekkersAuto.Web.Data.Models
{
    /// <summary>
    /// Represents a listing for a car
    /// </summary>
    public class Listing
    {
        /// <summary>
        /// The primary key of the listing
        /// </summary>
        [Key]
        public Guid Id { get; set; }
        /// <summary>
        /// Gets and sets the CarId
        /// Represents the id of the car the listing is for
        /// </summary>
        [ForeignKey("Car")]
        public Guid CarId { get; set; }
        /// <summary>
        /// Gets and sets the Car
        /// Virtual property containing the referenced Car
        /// </summary>
        public virtual Car Car { get; set; }
        /// <summary>
        /// Gets and sets the Description
        /// String description for a listing
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Gets and sets the Title
        /// Title of a listing
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Gets and sets the Images
        /// </summary>
        public virtual IEnumerable<Image> Images { get; set; }
    }
}
