using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Canabis.Models
{
    internal class Entreposage
    {
        [Key]
        public string idEntreposage { get; set; }
        public string nom { get; set; }
    }
}
