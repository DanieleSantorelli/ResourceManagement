using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DevOpsInspector.Data.Models
{
    public partial class VActivity
    {
        public string Progetto { get; set; }
        public string Team { get; set; }
        public string Utente { get; set; }
        public string NomeUtente { get; set; }
        public Guid? IdMembro { get; set; }
        public string Attività { get; set; }
        public double? CapacityPerDay { get; set; }
        public string Sprint { get; set; }
        public DateTime? InizioSprint { get; set; }
        public DateTime? FineSprint { get; set; }
        public string TipoSprint { get; set; }
    }
}
