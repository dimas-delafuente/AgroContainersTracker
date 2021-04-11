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
                    .HasName("Fruits_PK");

            entityBuilder.Property(e => e.FruitId).HasColumnType("int");

            entityBuilder.Property(e => e.Name)
                .HasColumnType("nvarchar(50)");

            entityBuilder.Property(e => e.Code)
                .HasColumnType("varchar(5)");

            entityBuilder.Property(e => e.Description)
                .HasColumnType("nvarchar(100)");
        }
    }
}