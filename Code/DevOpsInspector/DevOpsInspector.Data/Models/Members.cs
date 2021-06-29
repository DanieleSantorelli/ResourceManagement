using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DevOpsInspector.Data.Models
{
    public partial class Members
    {
        public Members()
        {
            MembersTeams = new HashSet<MembersTeams>();
        }

        public Guid Id { get; set; }
        public string Url { get; set; }
        public string AvatarUrl { get; set; }
        public string UniqueName { get; set; }
        public string ImageUrl { get; set; }
        public string Descriptor { get; set; }
        public string DisplayName { get; set; }
        public bool? IsTeamAdmin { get; set; }
        public DateTime DateCreation { get; set; }
        public DateTime? DateModify { get; set; }

        public virtual ICollection<MembersTeams> MembersTeams { get; set; }
    }
}
