using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeerstationFramework
{
    public class TempData
    {
        [JsonProperty("Weatherstation")]
        public string Weatherstation { get; set; }

        [JsonProperty("Timestamp")]
        public string Timestamp { get; set; }

        [JsonProperty("Temperature")]
        public string Temperature { get; set; }

        [JsonProperty("Illuminance")]
        public string Illuminance { get; set; }
    }
}