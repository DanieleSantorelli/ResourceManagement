using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DevOpsInspector.Data.Models
{
    public partial class ProjectAreaPath
    {
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }
        public Guid RootClassificationNodeId { get; set; }
        public DateTime DateCreation { get; set; }
        public DateTime? DateModify { get; set; }

        public virtual Projects Project { get; set; }
        public virtual RootClassificationNodes RootClassificationNode { get; set; }
    }
}
