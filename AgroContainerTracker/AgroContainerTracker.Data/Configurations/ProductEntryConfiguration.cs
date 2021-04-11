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

            entityBuilder.HasKey(e => new { e.CampaingId, e.ProductEntryNumber }).HasName("ProductEntries_PK");

            entityBuilder.Property(e => e.ProductEntryNumber).HasColumnType("int");

            entityBuilder.Property(e => e.CampaingId).HasColumnType("int");
            entityBuilder.HasOne(d => d.Campaing)
                .WithMany(p => p.ProductEntries)
                .HasForeignKey(d => d.CampaingId)
                .OnDelete(DeleteBehavior.Cascade);

            entityBuilder.Property(e => e.EntryDate)
                .HasColumnType("datetime");

            entityBuilder.Property(e => e.Closed)
                .HasDefaultValue(0)
                .HasColumnType("bit");

            entityBuilder.Property(e => e.HasProductExit)
                .HasDefaultValue(0)
                .HasColumnType("bit");

            entityBuilder.Property(e => e.Observations)
                .IsRequired(false)
                .HasColumnType("nvarchar(250)");

            entityBuilder.Property(e => e.HasHail)
                .HasColumnType("bit");
            entityBuilder.Property(e => e.HasPlague)
                .HasColumnType("bit");
            entityBuilder.Property(e => e.HasSecondPasses)
                .HasColumnType("bit");

            entityBuilder.Property(e => e.BuyerId)
                .IsRequired(false)
                .HasColumnType("int");

            entityBuilder.Property(e => e.PayerId)
                .IsRequired(false)
                .HasColumnType("int");

            entityBuilder.HasOne(d => d.Buyer)
                .WithMany(p => p.BuyerProductEntries)
                .HasForeignKey(d => d.BuyerId)
                .OnDelete(DeleteBehavior.Restrict);

            entityBuilder.HasOne(d => d.Payer)
                .WithMany(p => p.PayerProductEntries)
                .HasForeignKey(d => d.PayerId)
                .OnDelete(DeleteBehavior.Restrict);

            entityBuilder.HasMany(d => d.Sellers)
                .WithOne(p => p.ProductEntry)
                .HasForeignKey(d => new { d.CampaingId, d.ProductEntryNumber })
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
