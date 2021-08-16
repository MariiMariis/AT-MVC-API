using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Domain.Model.Models;

namespace Data.Data
{
    public class FabricantesContext : DbContext
    {
        public FabricantesContext (DbContextOptions<FabricantesContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(Console.WriteLine);

            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Domain.Model.Models.FabricanteModel> Fabricantes { get; set; }
        public DbSet<Domain.Model.Models.ProcessadorModel> Processadores { get; set; }
    }
}
