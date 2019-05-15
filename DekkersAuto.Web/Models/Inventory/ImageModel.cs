using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DekkersAuto.Web.Models.Inventory
{
    public class ImageModel
    {
        public Guid Id { get; set; }
        public string Source { get; set; }
        public Guid ListingId { get; set; }
        public bool IsFeature { get; set; }
    }
}
