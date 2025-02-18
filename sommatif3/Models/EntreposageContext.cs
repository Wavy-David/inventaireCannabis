using Microsoft.EntityFrameworkCore;
using sommatif3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Canabis.Models
{
    internal class EntreposageContext : DbContext
    {
        public EntreposageContext() { }
        // comment definir les tables
        public DbSet<Entreposage> Entreposage { get; set; }
        // defintion de la connexion
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

            => optionsBuilder.UseSqlServer("Data Source=DAVIDIL-LAPTOP-\\SQLEXPRESS01;Initial Catalog=Canabis;Integrated Security=True;Encrypt=False;Trust Server Certificate=True");

    }
}
