using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace sommatif3.Models
{
    public class medecin
    {
        [Key]
        public int MedecinId { get; set; }
        public string MedecinNom { get; set; }
        public string MedecinPrenom { get; set; }
        public int SpecialiteId { get; set; }
        public string SpecialiteNom { get; set; }
        public long  MedecinTelephone { get; set; }
        public decimal MedecinSalaire { get; set; }
    }
}
