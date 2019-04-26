using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DekkersAuto.Web.Models.Account
{
    /// <summary>
    /// Represents the properties required to view a banner record
    /// </summary>
    public class BannerViewModel
    {
        /// <summary>
        /// Gets and sets IsActive
        /// Represents whether the banner is active
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// Gets and sets the Text
        /// Represents the text that will be displayed in the banner
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// Gets and sets the bannerId
        /// Represents the Id of the banner
        /// </summary>
        public Guid? BannerId { get; set; }
    }
}
