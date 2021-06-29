using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DevOpsInspector.Data.Models
{
    public partial class Capacities
    {
        public Capacities()
        {
            Activities = new HashSet<Activities>();
            DaysOff = new HashSet<DaysOff>();
        }

        public Guid Id { get; set; }
        public string Url { get; set; }
        public Guid? MemberId { get; set; }
        public Guid? TeamId { get; set; }
        public Guid? IterationId { get; set; }
        public DateTime DateCreation { get; set; }
        public DateTime? DateModify { get; set; }

        public virtual Iterations Iteration { get; set; }
        public virtual Teams Team { get; set; }
        public virtual ICollection<Activities> Activities { get; set; }
        public virtual ICollection<DaysOff> DaysOff { get; set; }
    }
}
