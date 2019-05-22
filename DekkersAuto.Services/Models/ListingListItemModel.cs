using System;
using System.Collections.Generic;
using System.Text;

namespace DekkersAuto.Services.Models
{
    public class ListingListItemModel
    {
        public Guid ListingId { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string Title { get; set; }
        public int? Year { get; set; }
        public int? Kilometers { get; set; }
        public double? Price { get; set; }
    }
}
