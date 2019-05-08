using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DekkersAuto.Web.Data.Models
{
    public class ListingOption
    {
        [Key]
        public Guid Id { get; set; }
        [ForeignKey("Listing")]
        public Guid ListingId { get; set; }
        [ForeignKey("Option")]
        public Guid OptionId { get; set; }
    }
}
