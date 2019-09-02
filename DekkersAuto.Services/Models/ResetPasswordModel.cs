using System;
using System.Collections.Generic;
using System.Text;

namespace DekkersAuto.Services.Models
{
    public class ResetPasswordModel
    {
        public bool IsValid { get; set; }
        public string UserName { get; set; }
        public Guid ResetId { get; set; }
    }
}
