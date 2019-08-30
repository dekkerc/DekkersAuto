using System;
using System.Collections.Generic;
using System.Text;

namespace DekkersAuto.Services.Models
{
    /// <summary>
    /// Model representing the filter parameters
    /// </summary>
    public class FilterModel
    {
        /// <summary>
        /// Gets and sets the Make
        /// Represents the make of the vehicle to filter by
        /// </summary>
        public string Make { get; set; }
        /// <summary>
        /// Gets and sets the Model
        /// Represents the model to filter on
        /// </summary>
        public string Model { get; set; }
        /// <summary>
        /// Gets and sets the Colour
        /// Represents the colour to filter on
        /// </summary>
        public string Colour { get; set; }
        /// <summary>
        /// Gets and sets the YearFrom
        /// Represents the low end of a year filter range
        /// </summary>
        public virtual int? YearFrom { get; set; }
        /// <summary>
        /// Gets and sets the YearTo 
        /// Represents the upper end of a year filter range
        /// </summary>
        public virtual int? YearTo { get; set; }
        /// <summary>
        /// Gets and sets the KilometersFrom
        /// Represents the low end of the kilometer filter range
        /// </summary>
        public virtual int? KilometersFrom { get; set; }
        /// <summary>
        /// Gets and sets the KilometersTo
        /// Represents the upper end of the kilometer filter range
        /// </summary>
        public virtual int? KilometersTo { get; set; }
    }
}
