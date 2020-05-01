using AgroContainerTracker.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgroContainerTracker.Data.Configurations
{
    public class VehicleConfiguration : IEntityTypeConfiguration<VehicleEntity>
    {
        public void Configure(EntityTypeBuilder<VehicleEntity> entityBuilder)
        {
            entityBuilder.HasKey(e => e.VehicleId)
                    .HasName("PRIMARY");

            entityBuilder.HasIndex(e => e.CarrierId);

            entityBuilder.Property(e => e.VehicleId).HasColumnType("int(11)");

            entityBuilder.Property(e => e.CarriageRegistrationNumber)
                .HasColumnType("longtext")
                .HasCharSet("utf8mb4")
                .HasCollation("utf8mb4_general_ci");

            entityBuilder.Property(e => e.CarrierId).HasColumnType("int(11)");

            entityBuilder.Property(e => e.RegistrationNumber)
                .HasColumnType("longtext")
                .HasCharSet("utf8mb4")
                .HasCollation("utf8mb4_general_ci");

            entityBuilder.HasOne(d => d.CarrierCompany)
                .WithMany(p => p.Vehicles)
                .HasForeignKey(d => d.CarrierId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}