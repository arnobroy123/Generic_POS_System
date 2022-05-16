﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Generic_POS_System.Data
{
    public class PosContext : IdentityDbContext
    {
        public PosContext(DbContextOptions<PosContext> options)
            : base(options)
        {

        }

        public DbSet<Products> Product { get; set; }

        public DbSet<ProductArcade> ProductArcade { get; set; }
    }
}