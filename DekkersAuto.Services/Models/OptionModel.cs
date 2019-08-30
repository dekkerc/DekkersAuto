using System;
using System.Collections.Generic;
using System.Text;

namespace DekkersAuto.Services.Models
{
    /// <summary>
    /// Model to represent an option
    /// </summary>
    public class OptionModel
    {
        /// <summary>
        /// Gets and sets the Id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Gets and sets the Description
        /// </summary>
        public string Description { get; set; }
    }
}
