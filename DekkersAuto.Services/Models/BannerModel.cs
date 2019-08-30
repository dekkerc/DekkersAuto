using System;
using System.Collections.Generic;
using System.Text;

namespace DekkersAuto.Services.Models
{
    /// <summary>
    /// Model representing a banner
    /// </summary>
    public class BannerModel
    {
        /// <summary>
        /// Gets and sets the Text
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// Gets and sets IsActive
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// Gets and sets the BannerId
        /// </summary>
        public Guid? BannerId { get; set; }
    }
}
