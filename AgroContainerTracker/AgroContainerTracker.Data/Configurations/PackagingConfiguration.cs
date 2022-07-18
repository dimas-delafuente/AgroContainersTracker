using AgroContainerTracker.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgroContainerTracker.Data.Configurations
{
    public class PackagingConfiguration : IEntityTypeConfiguration<Packaging>
    {
        public void Configure(EntityTypeBuilder<Packaging> entityBuilder)
        {
            entityBuilder.ToTable("Packagings");

            entityBuilder.HasKey(e => e.Id)
                .HasName("Packagings_PK");

            entityBuilder.Property(e => e.Id)
                .HasColumnName("PackagingId")
                .HasColumnType("int");

            entityBuilder.Property(e => e.Code)
                .IsRequired()
                .HasColumnType("varchar(8)");

            entityBuilder.OwnsOne(e => e.Weight)
                .Property(p => p.Amount)
                    .HasConversion(
                        v => v.Value,
                        v => Amount.FromScalar(v)
                    )
                    .HasColumnType("decimal(7,5)")
                    .HasDefaultValueSql("0");

            entityBuilder.OwnsOne(e => e.Weight)
                .Property(p => p.Unit)
                    .HasConversion(
                        v => v.Name,
                        v => Enumeration.FromDisplayName<WeightUnit>(v)
                    )
                    .HasColumnType("varchar(5)")
                    .HasDefaultValueSql("kg");

            entityBuilder.Property(e => e.Description)
                .HasColumnType("nvarchar(100)");

            entityBuilder.Property(e => e.Color)
                .HasColumnType("nvarchar(25)");

            entityBuilder.Property(e => e.Total)
                .IsRequired()
                .HasColumnType("int");

            entityBuilder.Property(e => e.Active)
                .HasColumnType("bit");

            entityBuilder.Property(e => e.Type)
                .HasConversion(
                    v => v.Id,
                    v => Enumeration.FromValue<PackagingType>(v));

            entityBuilder.Property(e => e.Material)
                .HasConversion(
                    v => v.Id,
                    v => Enumeration.FromValue<PackagingMaterial>(v));

            entityBuilder.HasOne(d => d.Owner)
               .WithMany()
               .HasForeignKey("CompanyId")
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}