using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineShopping.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.Data
{
    public class ProjectContext:IdentityDbContext<Admin>
    {
        public ProjectContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {

            base.OnModelCreating(modelbuilder);

            modelbuilder.Entity<Admin>(entity =>
            {
                entity.ToTable(name: "Admins");
            });

            //aspnetroles
            modelbuilder.Entity<IdentityRole>(entity =>
            {
                entity.ToTable(name: "AdminRoles");
            });

            //aspnetuserroles
            modelbuilder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.ToTable("AdminAssignedRoles");
            });

           
        }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Item> Item { get; set; }
        public DbSet<Cart> Cart { get; set; }
    }
}
