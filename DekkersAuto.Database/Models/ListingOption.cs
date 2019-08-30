using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DekkersAuto.Database.Models
{
    /// <summary>
    /// Table relating an option to a listing
    /// </summary>
    public class ListingOption
    {
        /// <summary>
        /// Gets and sets the Id
        /// </summary>
        [Key]
        public Guid Id { get; set; }
        /// <summary>
        /// Gets and sets athe ListingId
        /// </summary>
        [ForeignKey("Listing")]
        public Guid ListingId { get; set; }
        /// <summary>
        /// Gets and sets the OptionId
        /// </summary>
        [ForeignKey("Option")]
        public Guid OptionId { get; set; }
    }
}
