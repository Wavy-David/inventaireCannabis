﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Canabis.Models
{
    class HistoriquePlanteContext : DbContext
    {
        public HistoriquePlanteContext() { }
        // comment definir les tables
        public DbSet<HistoriquePlante> HistoriquePlante { get; set; }
        // defintion de la connexion
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

            => optionsBuilder.UseSqlServer("Data Source=DAVIDIL-LAPTOP-\\SQLEXPRESS01;Initial Catalog=Canabis;Integrated Security=True;Encrypt=False;Trust Server Certificate=True");

    }
}
