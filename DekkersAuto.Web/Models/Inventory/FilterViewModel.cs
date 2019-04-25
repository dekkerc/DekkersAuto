using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DekkersAuto.Web.Models.Inventory
{
    /// <summary>
    /// Class holding parameters for filtering on vehicles
    /// </summary>
    public class FilterViewModel
    {
        /// <summary>
        /// Gets and sets the Make
        /// Represents the make of the vehicle to filter by
        /// </summary>
        public string Make { get; set; }
        /// <summary>
        /// Gets and sets the MakeList
        /// Represents the list of makes to select from
        /// </summary>
        public List<SelectListItem> MakeList { get; set; }
        /// <summary>
        /// Gets and sets the Model
        /// Represents the model to filter on
        /// </summary>
        public string Model { get; set; }
        /// <summary>
        /// Gets and sets the ModelList 
        /// Represents the ModelList to select from
        /// </summary>
        public List<SelectListItem> ModelList { get; set; }
        /// <summary>
        /// Gets and sets the Colour
        /// Represents the colour to filter on
        /// </summary>
        public string Colour { get; set; }
        /// <summary>
        /// Gets and sets the ColourList
        /// Represents the list of colours available
        /// </summary>
        public List<SelectListItem> ColourList { get; set; }
        /// <summary>
        /// Gets and sets the YearFrom
        /// Represents the low end of a year filter range
        /// </summary>
        public int YearFrom { get; set; }
        /// <summary>
        /// Gets and sets the YearTo 
        /// Represents the upper end of a year filter range
        /// </summary>
        public int YearTo { get; set; }
        /// <summary>
        /// Gets and sets the KilometersFrom
        /// Represents the low end of the kilometer filter range
        /// </summary>
        public int KilometersFrom { get; set; }
        /// <summary>
        /// Gets and sets the KilometersTo
        /// Represents the upper end of the kilometer filter range
        /// </summary>
        public int KilometersTo { get; set; }
    }
}
