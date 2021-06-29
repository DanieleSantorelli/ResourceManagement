using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DevOpsInspector.Data.Models
{
    public partial class VTeamMembers
    {
        public string Progetto { get; set; }
        public string Team { get; set; }
        public string Membro { get; set; }
        public bool InTeam { get; set; }
    }
}
