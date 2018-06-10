﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TransportBrunaWeb.Models;
using System.Data.Entity;

namespace TransportBrunaWeb.DAL
{
    public class BrunaContext : DbContext
    {
        public BrunaContext() : base("aspnet-TransportBrunaWeb-20180526110627")
        {
        }

        public DbSet<CargoTypes> CargoTypes { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<ContainerTypes> ContainerTypes { get; set; }
        public DbSet<CostTypes> CostTypes { get; set; }
        public DbSet<PrivateCustomer> PrivateCustomer { get; set; }
        public DbSet<TransportationStatusTypes> TransportationStatusTypes { get; set; }
        /*
        public DbSet<Costs> Costs { get; set; }
        public DbSet<Containers> Containers { get; set; }
        public DbSet<Vehicles> Vehicles { get; set; }
        public DbSet<VehicleCosts> VehicleCosts { get; set; }
        public DbSet<Customers> Customers { get; set; }
        public DbSet<TransportationLog> TransportationLog { get; set; }
        public DbSet<DrivingCosts> DrivingCosts { get; set; }
        public DbSet<TransportationStatus> TransportationStatus { get; set; }
        public DbSet<HouseholdTransportation> HouseholdTransportation { get; set; }
        */

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CargoTypes>().ToTable("CargoTypes");
            modelBuilder.Entity<Company>().ToTable("Company");
            modelBuilder.Entity<ContainerTypes>().ToTable("ContainerTypes");
            modelBuilder.Entity<CostTypes>().ToTable("CostTypes");
            modelBuilder.Entity<PrivateCustomer>().ToTable("PrivateCustomer");
            modelBuilder.Entity<TransportationStatusTypes>().ToTable("TransportationStatusTypes");
        }
    }
}