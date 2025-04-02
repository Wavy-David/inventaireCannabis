using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sommatif3.Models
{
    public class MedecinContext: DbContext
    {
        public MedecinContext() { }
        // comment definir les tables
        public DbSet<medecin> medecin { get; set; }
        // defintion de la connexion
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
       
            => optionsBuilder.UseSqlServer("Data Source=DESKTOP-LM7680U\\SQLEXPRESS01;Initial Catalog=hopital;Integrated Security=True;Encrypt=False;Trust Server Certificate=True");

    }
}
