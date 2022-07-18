using AgroContainerTracker.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgroContainerTracker.Data.Configurations
{
    public class PackagingMovementConfiguration : IEntityTypeConfiguration<PackagingMovement>
    {
        public void Configure(EntityTypeBuilder<PackagingMovement> entityBuilder)
        {
            entityBuilder.ToTable("PackagingMovements");

            entityBuilder.HasKey(e => e.Id)
                .HasName("PackagingMovements_PK");

            entityBuilder.Property(e => e.Id)
                .HasColumnName("PackagingMovementId")
                .HasColumnType("int");

            entityBuilder.Property(e => e.Operation)
                .IsRequired()
                .HasColumnType("tinyint");

            entityBuilder.Property(e => e.Quantity)
                .IsRequired()
                .HasColumnType("int");

            entityBuilder.Property(e => e.Total)
                .IsRequired()
                .HasColumnType("int");

            entityBuilder.Property(e => e.Created)
                .IsRequired()
                .HasColumnType("datetime");

            entityBuilder.HasOne(e => e.Packaging)
                .WithMany()
                .HasForeignKey("PackagingId")
                .OnDelete(DeleteBehavior.Cascade);

            entityBuilder.HasOne(e => e.Receiver)
                .WithMany()
                .HasForeignKey("CompanyId")
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}