
using KStore.Domain.Model;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KStore.Data
{
    public class EcommerceDbContext : DbContext
    {
        public EcommerceDbContext()
            : base("DefaultConnection")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUser>().HasKey(l => l.Id).ToTable("AspNetUsers");
            modelBuilder.Entity<IdentityUserLogin>().HasKey(u => new { u.UserId, u.ProviderKey, u.LoginProvider }).ToTable("AspNetUserLogins");
            modelBuilder.Entity<IdentityUserClaim>().HasKey(u => u.Id).ToTable("AspNetUserClaims");
            modelBuilder.Entity<User>().HasKey(l => l.Id).ToTable("AspNetUsers");
            modelBuilder.Entity<IdentityRole>().HasKey(r => r.Id).ToTable("AspNetRoles");
            modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId }).ToTable("AspNetUserRoles");
            modelBuilder.Entity<OrderLine>().HasKey(k => new { k.Order_Id, k.Product_Id });
            modelBuilder.Conventions.Add(new DateTime2Convention());
        }
        public DbSet<Product> Products { get; set; }

        public DbSet<ProductCategory> ProductCategories { get; set; }


        public DbSet<User> Users { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Brand> Brands { get; set; }

    }
}
