using System;
using System.Collections.Generic;
using System.Text;

namespace DekkersAuto.Services.Models
{
    public class ImageModel
    {
        public Guid Id { get; set; }
        public string Source { get; set; }
        public Guid ListingId { get; set; }
        public bool IsFeature { get; set; }
    }
}
