using AgroContainerTracker.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgroContainerTracker.Data.Configurations
{
    public class ProductEntryConfiguration : IEntityTypeConfiguration<ProductEntryEntity>
    {
        public void Configure(EntityTypeBuilder<ProductEntryEntity> entityBuilder)
        {
            entityBuilder.ToTable("ProductEntries");

            entityBuilder.HasKey(e => new { e.CampaingId, e.ProductEntryNumber });

            entityBuilder.Property(e => e.ProductEntryNumber).HasColumnType("int(11)");
            entityBuilder.Property(e => e.CampaingId).HasColumnType("int(11)");

            entityBuilder.Property(e => e.EntryDate)
                .HasColumnType("datetime");

            entityBuilder.Property(e => e.ExitDate)
                .IsRequired(false)
                .HasColumnType("datetime");

            entityBuilder.Property(e => e.Observations)
                .IsRequired(false)
                .HasColumnType("varchar(250)")
                .HasCharSet("utf8mb4")
                .HasCollation("utf8mb4_general_ci");

            entityBuilder.Property(e => e.HasHail)
                .HasColumnType("bit");
            entityBuilder.Property(e => e.HasPlague)
                .HasColumnType("bit");
            entityBuilder.Property(e => e.HasSecondPasses)
                .HasColumnType("bit");

            entityBuilder.Property(e => e.BuyerId)
                .IsRequired(false)
                .HasColumnType("int(11)");

            entityBuilder.Property(e => e.PayerId)
                .IsRequired(false)
                .HasColumnType("int(11)");

            entityBuilder.HasOne(d => d.Buyer)
                .WithMany(p => p.BuyerProductEntries)
                .HasForeignKey(d => d.BuyerId)
                .OnDelete(DeleteBehavior.SetNull);

            entityBuilder.HasOne(d => d.Payer)
                .WithMany(p => p.PayerProductEntries)
                .HasForeignKey(d => d.PayerId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
