using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DevOpsInspector.Data.Models
{
    public partial class VIterations
    {
        public string Progetto { get; set; }
        public string Team { get; set; }
        public string Sprint { get; set; }
        public DateTime? Inizio { get; set; }
        public DateTime? Fine { get; set; }
        public string TipoSprint { get; set; }
    }
}
