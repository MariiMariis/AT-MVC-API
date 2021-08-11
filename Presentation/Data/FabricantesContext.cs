﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Domain.Model.Models;

namespace Presentation.Data
{
    public class FabricantesContext : DbContext
    {
        public FabricantesContext (DbContextOptions<FabricantesContext> options)
            : base(options)
        {
        }

        public DbSet<Domain.Model.Models.FabricanteModel> FabricanteModel { get; set; }
    }
}
