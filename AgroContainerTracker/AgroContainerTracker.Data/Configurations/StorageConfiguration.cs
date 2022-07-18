using AgroContainerTracker.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgroContainerTracker.Data.Configurations
{
    public class StorageConfiguration : IEntityTypeConfiguration<Storage>
    {
        public void Configure(EntityTypeBuilder<Storage> entityBuilder)
        {
            entityBuilder.ToTable("Storages");

            entityBuilder.HasKey(e => e.Id).HasName("Storages_PK");

            entityBuilder.Property(e => e.Id)
                .HasColumnName("StorageId")
                .HasColumnType("int")
                .IsRequired();

            entityBuilder.Property(e => e.Number)
                .HasColumnType("int")
                .IsRequired();

            entityBuilder.Property(e => e.Description)
                .HasColumnType("varchar(100)");

            entityBuilder.Property(e => e.Surface)
                .HasColumnType("float")
                .IsRequired(false);

            entityBuilder.OwnsOne(e => e.Capacity)
                .Property(p => p.Amount)
                    .HasConversion(
                        v => v.Value,
                        v => Amount.FromScalar(v)
                    )
                    .HasColumnType("decimal(7,5)")
                    .HasDefaultValueSql("0");

            entityBuilder.OwnsOne(e => e.Capacity)
                .Property(p => p.Unit)
                    .HasConversion(
                        v => v.Name,
                        v => Enumeration.FromDisplayName<WeightUnit>(v)
                    )
                .HasColumnType("varchar(5)")
                .HasDefaultValueSql("kg");

            entityBuilder.Property(e => e.Temperature)
                .HasColumnType("float")
                .IsRequired(false);

            entityBuilder.Property(e => e.Humidity)
                .HasColumnType("float")
                .IsRequired(false);
        }
    }
}