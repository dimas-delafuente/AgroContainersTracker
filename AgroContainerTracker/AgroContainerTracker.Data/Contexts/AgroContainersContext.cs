using AgroContainerTracker.Data.Configurations;
using AgroContainerTracker.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;

namespace AgroContainerTracker.Data.Contexts
{
    public class ApplicationContext : IdentityDbContext
    {
        public virtual DbSet<Campaign> Campaigns { get; set; }
        public virtual DbSet<Carrier> Carriers { get; set; }
        public virtual DbSet<Carriage> Carriages { get; set; }
        public virtual DbSet<Storage> Storages { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<Creditor> Creditors { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Driver> Drivers { get; set; }
        public virtual DbSet<Fruit> Fruits { get; set; }
        public virtual DbSet<Input> Inputs { get; set; }
        public virtual DbSet<Packaging> Packagings { get; set; }
        public virtual DbSet<PackagingMovement> PackagingMovements { get; set; }
        public virtual DbSet<ProductRecord> ProductRecords { get; set; }
        public virtual DbSet<Weighing> Weighings { get; set; }
        public virtual DbSet<Rate> Rates { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<Vehicle> Vehicles { get; set; }

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
                var entries = ChangeTracker.Entries()
                    .Where(e => e.State == EntityState.Added ||
                                e.State == EntityState.Modified ||
                                e.State == EntityState.Deleted)
                    .ToList();

                foreach (var entry in entries)
                {
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
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
