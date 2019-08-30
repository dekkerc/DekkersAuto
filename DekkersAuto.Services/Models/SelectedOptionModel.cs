using System;
using System.Collections.Generic;
using System.Text;

namespace DekkersAuto.Services.Models
{
    /// <summary>
    /// Model to represent a selected option
    /// </summary>
    public class SelectedOptionModel : OptionModel
    {
        /// <summary>
        /// Gets and sets the ListingId
        /// </summary>
        public Guid ListingId { get; set; }
        /// <summary>
        /// Gets and sets Selected
        /// </summary>
        public bool Selected { get; set; }
    }
}
