using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DekkersAuto.Services.Models
{
    public class ModelResult
    {
        [JsonProperty("Make_ID")]
        public int MakeId { get; set; }
        [JsonProperty("Make_Name")]
        public string MakeName { get; set; }
        [JsonProperty("Model_ID")]
        public int ModelId { get; set; }
        [JsonProperty("Model_Name")]
        public string ModelName { get; set; }
    }
}
