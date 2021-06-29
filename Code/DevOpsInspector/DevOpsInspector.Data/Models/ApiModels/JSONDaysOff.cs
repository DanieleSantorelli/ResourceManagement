using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DevOpsInspector.Data.Models.ApiModels
{
    public class JSONDaysOff
    {
        [JsonPropertyName("start")]
        public DateTime? Start { get; set; }

        [JsonPropertyName("end")]
        public DateTime? End { get; set; }
    }
}
