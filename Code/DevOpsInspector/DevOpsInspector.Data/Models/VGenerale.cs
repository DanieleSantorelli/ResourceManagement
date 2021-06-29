using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DevOpsInspector.Data.Models
{
    public partial class VGenerale
    {
        public string ProjectName { get; set; }
        public string TeamName { get; set; }
        public string DisplayName { get; set; }
        public string UniqueName { get; set; }
        public bool? IsTeamAdmin { get; set; }
    }
}
