using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DekkersAuto.Web.Data.Models
{
    /// <summary>
    /// Represents a Banner displaying temporary information
    /// </summary>
    public class Banner
    {
        /// <summary>
        /// The primary key
        /// </summary>
        [Key]
        public Guid Id { get; set; }
        /// <summary>
        /// Gets and sets the Text
        /// Represents the text of the banner
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// Gets and sets IsActive
        /// Represents whether or not the banner should be shown
        /// </summary>
        public bool IsActive { get; set; }
    }
}
