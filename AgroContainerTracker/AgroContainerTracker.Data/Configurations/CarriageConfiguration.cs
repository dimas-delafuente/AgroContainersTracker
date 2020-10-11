using AgroContainerTracker.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgroContainerTracker.Data.Configurations
{
    public class CarriageConfiguration : IEntityTypeConfiguration<CarriageEntity>
    {
        public void Configure(EntityTypeBuilder<CarriageEntity> entityBuilder)
        {
            entityBuilder.ToTable("Carriages");

            entityBuilder.HasKey(e => e.CarriageId)
                    .HasName("PRIMARY");

            entityBuilder.HasIndex(e => e.CarrierId);

            entityBuilder.Property(e => e.CarriageId).HasColumnType("int(11)");

            entityBuilder.Property(e => e.CarrierId).HasColumnType("int(11)");

            entityBuilder.Property(e => e.CarriageRegistrationNumber)
                .HasColumnType("longtext")
                .HasCharSet("utf8mb4")
                .HasCollation("utf8mb4_general_ci");

            entityBuilder.HasOne(d => d.CarrierCompany)
                .WithMany(p => p.Carriages)
                .HasForeignKey(d => d.CarrierId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
