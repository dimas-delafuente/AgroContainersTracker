using AgroContainerTracker.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgroContainerTracker.Data.Configurations
{
    public class VehicleConfiguration : IEntityTypeConfiguration<VehicleEntity>
    {
        public void Configure(EntityTypeBuilder<VehicleEntity> entityBuilder)
        {
            entityBuilder.ToTable("Vehicles");

            entityBuilder.HasKey(e => e.VehicleId)
                    .HasName("Vehicles_PK");

            entityBuilder.HasIndex(e => e.CarrierId);

            entityBuilder.Property(e => e.VehicleId).HasColumnType("int");

            entityBuilder.Property(e => e.CarrierId).HasColumnType("int");

            entityBuilder.Property(e => e.RegistrationNumber)
                .HasColumnType("varchar(15)");

            entityBuilder.HasOne(d => d.CarrierCompany)
                .WithMany(p => p.Vehicles)
                .HasForeignKey(d => d.CarrierId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}