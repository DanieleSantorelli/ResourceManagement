using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DevOpsInspector.Data.Models.ApiModels
{
    public class Self
    {
        [JsonPropertyName("href")]
        public string Href { get; set; }
    }
}
