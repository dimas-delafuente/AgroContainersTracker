using AgroContainerTracker.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgroContainerTracker.Data.Configurations
{
    public class FruitConfiguration : IEntityTypeConfiguration<FruitEntity>
    {
        public void Configure(EntityTypeBuilder<FruitEntity> entityBuilder)
        {
            entityBuilder.ToTable("Fruits");

            entityBuilder.HasKey(e => e.FruitId)
                    .HasName("PRIMARY");

            entityBuilder.Property(e => e.FruitId).HasColumnType("int(11)");

            entityBuilder.Property(e => e.Name)
                .HasColumnType("longtext")
                .HasCharSet("utf8mb4")
                .HasCollation("utf8mb4_general_ci");

            entityBuilder.Property(e => e.Code)
                .HasColumnType("varchar(5)")
                .HasCharSet("utf8mb4")
                .HasCollation("utf8mb4_general_ci");

            entityBuilder.Property(e => e.Description)
                .HasColumnType("varchar(100)")
                .HasCharSet("utf8mb4")
                .HasCollation("utf8mb4_general_ci");
        }
    }
}