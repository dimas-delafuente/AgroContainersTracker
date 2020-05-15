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
                .HasName("PRIMARY");


            entityBuilder.Property(e => e.PackagingId).HasColumnType("int(11)");

            entityBuilder.Property(e => e.Code)
                .IsRequired()
                .HasColumnType("varchar(8)")
                .HasCharSet("utf8mb4")
                .HasCollation("utf8mb4_general_ci");


            entityBuilder.Property(e => e.Weight)
                .IsRequired()
                .HasColumnType("decimal(6,3)");

            entityBuilder.Property(e => e.Description)
                .HasColumnType("varchar(100)")
                .HasCharSet("utf8mb4")
                .HasCollation("utf8mb4_general_ci");

            entityBuilder.Property(e => e.Color)
                .HasColumnType("varchar(25)")
                .HasCharSet("utf8mb4")
                .HasCollation("utf8mb4_general_ci");

            entityBuilder.Property(e => e.Total)
                .IsRequired()
                .HasColumnType("int(11)");

            entityBuilder.Property(e => e.Active)
                .HasColumnType("bit");

            entityBuilder.Property(e => e.Type)
                .IsRequired()
                .HasColumnType("tinyint");

            entityBuilder.Property(e => e.Material)
                .IsRequired()
                .HasColumnType("tinyint");

            entityBuilder.Property(e => e.CustomerId)
                .HasColumnType("int(11)")
                .IsRequired(false);

            entityBuilder.HasOne(d => d.Owner)
                .WithMany(p => p.Packagings)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}