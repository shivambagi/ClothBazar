using ClothBazar.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothBazar.DataBase
{
    public class CBContext : DbContext,IDisposable
    {
        public CBContext():base("ClothBazarConnection")
        {

        }

        //// use this for Fluent API, override OnModelCreating from CBContext
        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Category>().Property(p => p.Name).IsRequired().HasMaxLength(50);
        //}

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Config> Configurations { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        //-------------------------------------------------------------------------------------
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Frequency> Frequencies { get; set; }
        public DbSet<InsuranceProduct> InsuranceProducts { get; set; }
        public DbSet<ULIP> ULIPS { get; set; }

    }
}
