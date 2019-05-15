using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DekkersAuto.Web.Models.Inventory
{
    public class OptionModel
    {
        public Guid ListingId { get; set; }
        public Guid Id { get; set; }
        public string Description { get; set; }
        public bool Selected { get; set; } = false;
    }
}
