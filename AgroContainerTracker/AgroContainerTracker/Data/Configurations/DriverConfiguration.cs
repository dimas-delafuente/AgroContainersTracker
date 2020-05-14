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
                    .HasName("PRIMARY");

            entityBuilder.HasIndex(e => e.CarrierId);

            entityBuilder.Property(e => e.DriverId).HasColumnType("int(11)");

            entityBuilder.Property(e => e.CarrierId).HasColumnType("int(11)");

            entityBuilder.Property(e => e.IdentificationNumber)
                .HasColumnType("longtext")
                .HasCharSet("utf8mb4")
                .HasCollation("utf8mb4_general_ci");

            entityBuilder.Property(e => e.Name)
                .HasColumnType("longtext")
                .HasCharSet("utf8mb4")
                .HasCollation("utf8mb4_general_ci");

            entityBuilder.HasOne(d => d.CarrierCompany)
                .WithMany(p => p.Drivers)
                .HasForeignKey(d => d.CarrierId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}