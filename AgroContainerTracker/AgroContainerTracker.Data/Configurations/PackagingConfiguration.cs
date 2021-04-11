using AgroContainerTracker.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgroContainerTracker.Data.Configurations
{
    public class PackagingConfiguration : IEntityTypeConfiguration<PackagingEntity>
    {
        public void Configure(EntityTypeBuilder<PackagingEntity> entityBuilder)
        {
            entityBuilder.ToTable("Packagings");

            entityBuilder.HasKey(e => e.PackagingId)
                .HasName("Packagings_PK");

            entityBuilder.Property(e => e.PackagingId).HasColumnType("int");

            entityBuilder.Property(e => e.Code)
                .IsRequired()
                .HasColumnType("varchar(8)");

            entityBuilder.Property(e => e.Weight)
                .IsRequired()
                .HasColumnType("decimal(6,3)");

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
                .IsRequired()
                .HasColumnType("tinyint");

            entityBuilder.Property(e => e.Material)
                .IsRequired()
                .HasColumnType("tinyint");

            entityBuilder.Property(e => e.CustomerId)
                .HasColumnType("int")
                .IsRequired(false);

            entityBuilder.HasOne(d => d.Owner)
                .WithMany(p => p.Packagings)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            entityBuilder.HasMany(d => d.PackagingMovements)
                .WithOne(p => p.Packaging)
                .HasForeignKey(d => d.PackagingId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}