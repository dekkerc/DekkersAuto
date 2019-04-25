using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DekkersAuto.Web.Data.Models
{
    /// <summary>
    /// Class representing a manufacturer of cars
    /// </summary>
    public class Make
    {
        /// <summary>
        /// Gets and sets the Id
        /// Represents the primary key of the Make
        /// </summary>
        [Key]
        public Guid Id { get; set; }
        /// <summary>
        /// Gets and sets the Name
        /// Represents the name of the manufacturer
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Gets and sets the Models
        /// Represents the models associated with this Make
        /// </summary>
        public virtual List<Model> Models { get; set; }
    }
}
