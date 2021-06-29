using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DevOpsInspector.Data.Models
{
    public partial class RootClassificationNodes
    {
        public RootClassificationNodes()
        {
            ProjectAreaPath = new HashSet<ProjectAreaPath>();
        }

        public Guid Id { get; set; }
        public int Identifier { get; set; }
        public string Name { get; set; }
        public string StructureType { get; set; }
        public bool HasChildren { get; set; }
        public string Path { get; set; }
        public string Url { get; set; }
        public DateTime DateCreation { get; set; }
        public DateTime? DateModify { get; set; }

        public virtual ICollection<ProjectAreaPath> ProjectAreaPath { get; set; }
    }
}
