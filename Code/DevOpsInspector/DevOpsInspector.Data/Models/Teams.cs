using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DevOpsInspector.Data.Models
{
    public partial class Teams
    {
        public Teams()
        {
            Capacities = new HashSet<Capacities>();
            Iterations = new HashSet<Iterations>();
            MembersTeams = new HashSet<MembersTeams>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string IdentityUrl { get; set; }
        public string ProjectName { get; set; }
        public Guid? ProjectId { get; set; }
        public DateTime DateCreation { get; set; }
        public DateTime? DateModify { get; set; }

        public virtual Projects Project { get; set; }
        public virtual ICollection<Capacities> Capacities { get; set; }
        public virtual ICollection<Iterations> Iterations { get; set; }
        public virtual ICollection<MembersTeams> MembersTeams { get; set; }
    }
}
