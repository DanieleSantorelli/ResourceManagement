using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DevOpsInspector.Data.Models
{
    public partial class DaysOff
    {
        public Guid Id { get; set; }
        public Guid? CapacityId { get; set; }
        public DateTime DateCreation { get; set; }
        public DateTime? DateModify { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }

        public virtual Capacities Capacity { get; set; }
    }
}
