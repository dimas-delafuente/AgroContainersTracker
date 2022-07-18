using AgroContainerTracker.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgroContainerTracker.Data.Configurations
{
    public class FruitConfiguration : IEntityTypeConfiguration<Fruit>
    {
        public void Configure(EntityTypeBuilder<Fruit> entityBuilder)
        {
            entityBuilder.ToTable("Fruits");

            entityBuilder.HasKey(e => e.Id)
                    .HasName("Fruits_PK");

            entityBuilder.Property(e => e.Id)
                .HasColumnName("FruitId")
                .HasColumnType("int");

            entityBuilder.Property(e => e.Name)
                .HasColumnType("nvarchar(50)");

            entityBuilder.Property(e => e.Code)
                .HasColumnType("varchar(5)");

            entityBuilder.Property(e => e.Description)
                .HasColumnType("nvarchar(100)");
        }
    }
}