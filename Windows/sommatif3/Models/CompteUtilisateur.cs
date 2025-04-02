using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Canabis.Models
{
    class CompteUtilisateur
    {
        [Key]
        public string IdUtilisateur { get; set; }
        public string Prenom { get; set; }
        public string Nom { get; set; }
        public string Email { get; set; }
        // Optional property for role (uncomment if needed)
        // public string Role { get; set; }
        public string motdepass { get; set; } // Consider using a secure hashing mechanism for password storage
    }
}
