using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DevOpsInspector.Data.Models.ApiModels
{
    public class Links
    {
        [JsonPropertyName("avatar")]
        public Avatar avatar { get; set; }

        [JsonPropertyName("self")]
        public Self Self { get; set; }
    }
}
