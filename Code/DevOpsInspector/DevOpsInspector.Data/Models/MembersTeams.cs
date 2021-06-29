using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DevOpsInspector.Data.Models
{
    public partial class MembersTeams
    {
        public Guid Id { get; set; }
        public Guid? TeamId { get; set; }
        public Guid? MemberId { get; set; }
        public bool? IsInTeam { get; set; }
        public DateTime DateCreation { get; set; }
        public DateTime? DateModify { get; set; }

        public virtual Members Member { get; set; }
        public virtual Teams Team { get; set; }
    }
}
