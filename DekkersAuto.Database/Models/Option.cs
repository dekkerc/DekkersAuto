using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DekkersAuto.Database.Models
{
    /// <summary>
    /// Class describing a feature of a car
    /// </summary>
    public class Option
    {
        /// <summary>
        /// Gets and sets the Id
        /// </summary>
        [Key]
        public Guid Id { get; set; }
        /// <summary>
        /// Gets and sets the Description
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Gets and sets the ListingOptions
        /// </summary>
        public virtual ICollection<ListingOption> ListingOptions { get; set; }
    }
}
