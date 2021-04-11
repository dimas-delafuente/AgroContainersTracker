using AgroContainerTracker.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgroContainerTracker.Data.Configurations
{
    public class DriverConfiguration : IEntityTypeConfiguration<DriverEntity>
    {
        public void Configure(EntityTypeBuilder<DriverEntity> entityBuilder)
        {
            entityBuilder.ToTable("Drivers");


            entityBuilder.HasKey(e => e.DriverId)
                    .HasName("Drivers_PK");

            entityBuilder.HasIndex(e => e.CarrierId);

            entityBuilder.Property(e => e.DriverId).HasColumnType("int");

            entityBuilder.Property(e => e.CarrierId).HasColumnType("int");

            entityBuilder.Property(e => e.IdentificationNumber)
                .HasColumnType("varchar(15)");

            entityBuilder.Property(e => e.Name)
                .HasColumnType("varchar(100)");

            entityBuilder.HasOne(d => d.CarrierCompany)
                .WithMany(p => p.Drivers)
                .HasForeignKey(d => d.CarrierId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}