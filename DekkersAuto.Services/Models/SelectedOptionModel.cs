using System;
using System.Collections.Generic;
using System.Text;

namespace DekkersAuto.Services.Models
{
    public class SelectedOptionModel : OptionModel
    {
        public Guid ListingId { get; set; }
        public bool Selected { get; set; }
    }
}
