using AgroContainerTracker.Data.Entities;
using Microsoft.EntityFrameworkCore;
using AgroContainerTracker.Domain;

namespace AgroContainerTracker.Data.Contexts
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }

        public DbSet<ContainerEntity> Containers { get; set; }

        public DbSet<PalotEntity> Palots { get; set; }

        public DbSet<FruitEntity> Fruits { get; set; }

        public DbSet<FruitVendorEntity> FruitVendors { get; set; }

        public DbSet<AgroContainerTracker.Domain.Container> Container { get; set; }

    }
}
