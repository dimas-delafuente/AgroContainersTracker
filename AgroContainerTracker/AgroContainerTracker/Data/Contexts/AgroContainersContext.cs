using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using AgroContainerTracker.Data.Entities;
using System.Reflection;

namespace AgroContainerTracker.Data.Contexts
{
    public partial class ApplicationContext : DbContext
    {
        public ApplicationContext()
        {
        }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CarrierEntity> Carriers { get; set; }
        public virtual DbSet<ContainerEntity> Containers { get; set; }
        public virtual DbSet<CountryEntity> Countries { get; set; }
        public virtual DbSet<CreditorEntity> Creditors { get; set; }
        public virtual DbSet<CustomerEntity> Customers { get; set; }
        public virtual DbSet<DriverEntity> Drivers { get; set; }
        public virtual DbSet<FruitEntity> Fruits { get; set; }
        public virtual DbSet<PalotEntity> Palots { get; set; }
        public virtual DbSet<RateEntity> RateEntity { get; set; }
        public virtual DbSet<SupplierEntity> Suppliers { get; set; }
        public virtual DbSet<VehicleEntity> Vehicles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder?.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
