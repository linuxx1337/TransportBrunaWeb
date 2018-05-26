using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TransportBrunaWeb.Models;
using System.Data.Entity;

namespace TransportBrunaWeb.DAL
{
    public class BrunaContext : DbContext
    {
        public BrunaContext() : base("BrunaContext")
        {
        }

        public DbSet<CargoTypes> CargoTypes { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<ContainerTypes> ContainerTypes { get; set; }
        public DbSet<CostTypes> CostTypes { get; set; }
        public DbSet<PrivateCustomer> PrivateCustomer { get; set; }
        public DbSet<TransportationStatusTypes> TransportationStatusTypes { get; set; }
        
        /*protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Company>().ToTable("Company")
            //modelBuilder.Entity<PrivateCustomer>().ToTable("PrivateCustomer")
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }*/
    }
}