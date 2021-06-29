using System;
using System.Collections.Generic;
using System.Text;

namespace DevOpsInspector.Data.Models.ApiModels
{
    public class JSONProject
    {
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string url { get; set; }
        public string state { get; set; }
        public int revision { get; set; }
        public string visibility { get; set; }
        public DateTime lastUpdateTime { get; set; }
    }
}
