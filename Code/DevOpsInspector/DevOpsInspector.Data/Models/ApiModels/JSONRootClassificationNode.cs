using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DevOpsInspector.Data.Models.ApiModels
{
    public class JSONRootClassificationNode
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("identifier")]
        public string Identifier { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("structureType")]
        public string StructureType { get; set; }

        [JsonPropertyName("hasChildren")]
        public bool HasChildren { get; set; }

        [JsonPropertyName("path")]
        public string Path { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }
    }
}
