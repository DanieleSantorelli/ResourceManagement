using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DevOpsInspector.Data.Models.ApiModels
{
    public class JSONCapacities
    {
        [JsonPropertyName("teamMember")]
        public Identity TeamMember { get; set; }

        [JsonPropertyName("activities")]
        public List<Activity> Activities { get; set; }

        [JsonPropertyName("daysOff")]
        public List<JSONDaysOff> DaysOff { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        public Guid teamId { get; set; }
        public Guid iterationId { get; set; }
    }

    public class Activity
    {
        [JsonPropertyName("capacityPerDay")]
        public double CapacityPerDay { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
