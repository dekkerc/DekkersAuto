using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DekkersAuto.Web.Data.Models
{
    public class Option
    {
        [Key]
        public Guid Id { get; set; }
        [ForeignKey("Car")]
        public Guid CarId { get; set; }
        public string Description { get; set; }
    }
}
