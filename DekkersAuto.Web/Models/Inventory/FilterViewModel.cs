using DekkersAuto.Services.Models;
using System.ComponentModel.DataAnnotations;

namespace DekkersAuto.Web.Models.Inventory
{
    /// <summary>
    /// Class holding parameters for filtering on vehicles
    /// </summary>
    public class FilterViewModel : FilterModel
    {
        /// <summary>
        /// Gets and sets the YearFrom
        /// Represents the low end of a year filter range
        /// </summary>
        [Display(Name = "From", ResourceType = typeof(Locale))]
        public override int? YearFrom { get; set; }
        /// <summary>
        /// Gets and sets the YearTo 
        /// Represents the upper end of a year filter range
        /// </summary>
        [Display(Name = "To", ResourceType = typeof(Locale))]
        public override int? YearTo { get; set; }
        /// <summary>
        /// Gets and sets the KilometersFrom
        /// Represents the low end of the kilometer filter range
        /// </summary>
        [Display(Name ="From", ResourceType = typeof(Locale))]
        public override int? KilometersFrom { get; set; }
        /// <summary>
        /// Gets and sets the KilometersTo
        /// Represents the upper end of the kilometer filter range
        /// </summary>
        [Display(Name = "To", ResourceType = typeof(Locale))]
        public override int? KilometersTo { get; set; }
    }
}
