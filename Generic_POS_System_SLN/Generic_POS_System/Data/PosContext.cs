using Generic_POS_System.Mdoels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Generic_POS_System.Data
{
    public class PosContext : IdentityDbContext<AppUser>
    {
        public PosContext(DbContextOptions<PosContext> options)
            : base(options)
        {

        }

        public DbSet<Products> Product { get; set; }

        public DbSet<ProductArcade> ProductArcade { get; set; }

        public DbSet<Category> Category { get; set; }

        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<Cart> Cart  { get; set; }


    }
}
