using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DevOpsInspector.Data.Models
{
    public partial class Projects
    {
        public Projects()
        {
            ProjectAreaPath = new HashSet<ProjectAreaPath>();
            Teams = new HashSet<Teams>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string State { get; set; }
        public int? Revision { get; set; }
        public string Visibility { get; set; }
        public DateTime? LastUpdateTime { get; set; }
        public DateTime DateCreation { get; set; }
        public DateTime? DateModify { get; set; }

        public virtual ICollection<ProjectAreaPath> ProjectAreaPath { get; set; }
        public virtual ICollection<Teams> Teams { get; set; }
    }
}
