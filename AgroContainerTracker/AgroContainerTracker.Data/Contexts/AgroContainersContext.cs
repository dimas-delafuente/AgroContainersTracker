using System;
using AgroContainerTracker.Data.Entities;
using System.Reflection;
using System.Linq;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AgroContainerTracker.Data.Contexts
{
    public partial class ApplicationContext : IdentityDbContext
    {

        public virtual DbSet<CarrierEntity> Carriers { get; set; }
        public virtual DbSet<ColdRoomEntity> ColdRooms { get; set; }
        public virtual DbSet<CountryEntity> Countries { get; set; }
        public virtual DbSet<CreditorEntity> Creditors { get; set; }
        public virtual DbSet<CustomerEntity> Customers { get; set; }
        public virtual DbSet<DriverEntity> Drivers { get; set; }
        public virtual DbSet<FruitEntity> Fruits { get; set; }
        public virtual DbSet<RateEntity> Rates { get; set; }
        public virtual DbSet<SupplierEntity> Suppliers { get; set; }
        public virtual DbSet<VehicleEntity> Vehicles { get; set; }
        public virtual DbSet<PackagingEntity> Packagings { get; set; }
        public virtual DbSet<PackagingMovementEntity> PackagingMovements { get; set; }
        public virtual DbSet<CampaingEntity> Campaings { get; set; }
        public virtual DbSet<ProductEntryEntity> ProductEntries { get; set; }
        public virtual DbSet<ProductEntrySellerEntity> ProductEntrySellers { get; set; }
        public virtual DbSet<ProductWeighingEntity> ProductWeighings { get; set; }
        public virtual DbSet<ProductRecordEntity> ProductRecords { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder?.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

        public void DetachAll()
        {
            try
            {
                var entries = this.ChangeTracker.Entries()
                    .Where(e => e.State == EntityState.Added ||
                                e.State == EntityState.Modified ||
                                e.State == EntityState.Deleted)
                    .ToList();

                foreach (var entry in entries)
                    switch (entry.State)
                    {
                        case EntityState.Modified:
                            entry.State = EntityState.Unchanged;
                            break;
                        case EntityState.Added:
                            entry.State = EntityState.Detached;
                            break;
                        case EntityState.Deleted:
                            entry.Reload();
                            break;
                    }
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
