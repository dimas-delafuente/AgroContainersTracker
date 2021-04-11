using AgroContainerTracker.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgroContainerTracker.Data.Configurations
{
    public class PackagingMovementConfiguration : IEntityTypeConfiguration<PackagingMovementEntity>
    {
        public void Configure(EntityTypeBuilder<PackagingMovementEntity> entityBuilder)
        {
            entityBuilder.ToTable("PackagingMovements");


            entityBuilder.HasKey(e => e.PackagingMovementId)
                .HasName("PackagingMovements_PK");


            entityBuilder.Property(e => e.PackagingMovementId).HasColumnType("int");

            entityBuilder.Property(e => e.Operation)
                .IsRequired()
                .HasColumnType("tinyint");

            entityBuilder.Property(e => e.Amount)
                .IsRequired()
                .HasColumnType("int");

            entityBuilder.Property(e => e.Total)
                .IsRequired()
                .HasColumnType("int");

            entityBuilder.Property(e => e.Created)
                .IsRequired()
                .HasColumnType("datetime");


            entityBuilder.Property(e => e.PackagingId).HasColumnType("int");

            entityBuilder.HasOne(d => d.Packaging)
                .WithMany(p => p.PackagingMovements)
                .HasForeignKey(d => d.PackagingId)
                .OnDelete(DeleteBehavior.Cascade);

            entityBuilder.Property(e => e.CustomerId)
                .HasColumnType("int")
                .IsRequired(false);

            entityBuilder.HasOne(d => d.Customer)
                .WithMany(p => p.PackagingMovements)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}