using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DekkersAuto.Database.Models
{
    /// <summary>
    /// Class describing a feature of a car
    /// </summary>
    public class Option
    {
        [Key]
        public Guid Id { get; set; }
        public string Description { get; set; }
    }
}
