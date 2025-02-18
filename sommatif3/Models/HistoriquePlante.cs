using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Canabis.Models
{
    class HistoriquePlante
    {
        [Key]
        public int Id { get; set; }
        public string IdPlante { get; set; }
        public string Action { get; set; }
        public DateTime Date { get; set; }
        public string Champ { get; set; }
        public string AncienneValeur { get; set; }
        public string NouvelleValeur { get; set; }
    }
}
