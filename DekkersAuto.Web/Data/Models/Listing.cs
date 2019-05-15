using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DekkersAuto.Web.Data.Models
{
    /// <summary>
    /// Represents a listing for a car
    /// </summary>
    public class Listing
    {
        /// <summary>
        /// The primary key of the listing
        /// </summary>
        [Key]
        public Guid Id { get; set; }
        /// <summary>
        /// Gets and sets the Make
        /// Represents the make of a car
        /// </summary>
        public string Make { get; set; }
        /// <summary>
        /// Gets and sets the model
        /// Represents the model of a car
        /// </summary>
        public string Model { get; set; }
        /// <summary>
        /// Gets and sets the Kilometers
        /// Represents the mileage of a car
        /// </summary>
        public int? Kilometers { get; set; }
        /// <summary>
        /// Gets and sets the Colour
        /// Represents the colour of a car
        /// </summary>
        public string Colour { get; set; }
        /// <summary>
        /// Gets and sets the FuelType
        /// Represents the fuel type of a car
        /// </summary>
        public string FuelType { get; set; }
        /// <summary>
        /// Gets and sets the Transmission
        /// Represents the Transmission type of a car
        /// </summary>
        public string Transmission { get; set; }
        /// <summary>
        /// Gets and sets the DriveTrain
        /// Represents the DriveTrain type of a car
        /// </summary>
        public string DriveTrain { get; set; }
        /// <summary>
        /// Gets and sets the Year
        /// Represents the year of a car
        /// </summary>
        public int? Year { get; set; }
        /// <summary>
        /// Gets and sets the BodyType
        /// Represents the body type of a car
        /// </summary>
        public string BodyType { get; set; }
        /// <summary>
        /// Gets and sets the Doors
        /// Represents the number of doors of a car
        /// </summary>
        public int? Doors { get; set; }
        /// <summary>
        /// Gets and sets the Seats
        /// Represents the number of seats of a car
        /// </summary>
        public int? Seats { get; set; }
        /// <summary>
        /// Gets and sets the Description
        /// String description for a listing
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Gets and sets the Title
        /// Title of a listing
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Gets and sets the Images
        /// </summary>
        public virtual IEnumerable<Image> Images { get; set; }
        /// <summary>
        /// Gets and sets the Price 
        /// </summary>
        public double? Price { get; set; }
        /// <summary>
        /// Gets and sets IsActive
        /// Represents if the listing should be shown with the active listings
        /// </summary>
        public bool IsActive { get; set; } = false;
    }
}
