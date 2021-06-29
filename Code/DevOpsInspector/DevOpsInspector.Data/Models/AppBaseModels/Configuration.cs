using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DevOpsInspector.Data.Models.AppBaseModels
{
    public class Configuration
    {
        #region public fields
        [JsonPropertyName("CRON_SCHEDULE")]
        public string CronSchedule { get; set; }

        [JsonPropertyName("DEVOPS_PERSONAL_ACCESS_TOKEN")]
        public string AccessToken { get; set; }

        [JsonPropertyName("DEVOPS_ORGANIZATION_BASE_URI")]
        public string OrganizzationBaseUri { get; set; }

        [JsonPropertyName("PROJECTS_URI")]
        public string ProjectsUri { get; set; }

        [JsonPropertyName("CLASSIFICATION_NODES_ROOT_URI")]
        public string ClassificationNodesRootUri { get; set; }

        [JsonPropertyName("CLASSIFICATION_NODE_URI")]
        public string ClassificationNodeUri { get; set; }

        [JsonPropertyName("TEAMS_URI")]
        public string TeamsUri { get; set; }

        [JsonPropertyName("MEMBERS_URI")]
        public string MembersUri { get; set; }

        [JsonPropertyName("ITERATIONS_URI")]
        public string IterationsUri { get; set; }

        [JsonPropertyName("CAPACITIES_URI")]
        public string CapacitiesUri { get; set; }

        [JsonPropertyName("TEAMS_DAYS_OFF")]
        public string TeamsDaysOff { get; set; }

        [JsonPropertyName("CONFIGURATION_PROJECTS")]
        public List<string> ConfigurationProjects { get; set; }
        #endregion public fields
    }
}
