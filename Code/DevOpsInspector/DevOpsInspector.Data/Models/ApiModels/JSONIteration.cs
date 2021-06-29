using System;
using System.Collections.Generic;
using System.Text;

namespace DevOpsInspector.Data.Models.ApiModels
{
    public class Attributes
    {
        public DateTime? startDate { get; set; }
        public DateTime? finishDate { get; set; }
        public string timeFrame { get; set; }
    }

    public class JSONIteration
    {
        public string id { get; set; }
        public string name { get; set; }
        public string path { get; set; }
        public Attributes attributes { get; set; }
        public string url { get; set; }
    }

}