using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DekkersAuto.Web.Data.Models
{
    /// <summary>
    /// Represents the model of a car
    /// </summary>
    public class Model
    {
        /// <summary>
        /// Gets and sets the Id
        /// Represents the primary key of the model
        /// </summary>
        [Key]
        public Guid Id { get; set; }
        /// <summary>
        /// Gets and sets the Name
        /// Represents the name of the model
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Gets and sets the MakeId
        /// Represents the id of the maker of this model
        /// </summary>
        [ForeignKey("Make")]
        public Guid MakeId { get; set; }
        /// <summary>
        /// Gets and sets the Make
        /// Represents the maker of the model
        /// </summary>
        public virtual Make Make { get; set; }
    }
}
