using System;
using System.Collections.Generic;
using System.Text;

namespace DekkersAuto.Services.Models
{
    public class BannerModel
    {
        public string Text { get; set; }
        public bool IsActive { get; set; }
        public Guid? BannerId { get; set; }
    }
}
