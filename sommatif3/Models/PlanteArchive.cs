using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Canabis.Models
{
    internal class PlanteArchive
    {
        [Key]
        public string IdPlante { get; set; }
        public string EtatSante { get; set; }
        public DateTime DateAjout { get; set; }
        public string Provenance { get; set; }
        public string Description { get; set; }
        public string Stade { get; set; }
        public string Entreposage { get; set; }
        public int Active_Inactive { get; set; }
        public string ItemRetireInventaire { get; set; }
        public string Note { get; set; }
        //public string CodeQr { get; set; }
        public string Responsable { get; set; }

        //public DateTime DateAjout { get; set; } = DateTime.Now;

        public DateTime DateRetrait { get; set; }
    }
}
