using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DekkersAuto.Services.Models
{
    public class MakeResult
    {
        [JsonProperty("Make_ID")]
        public int MakeId { get; set; }
        [JsonProperty("Make_Name")]
        public string MakeName { get; set; }
        [JsonProperty("Vehicle_Type_ID")]
        public int VehicleTypeId { get; set; }
        [JsonProperty("Vehicle_Type_Name")]
        public string VehicleTypeName { get; set; }
    }
}
