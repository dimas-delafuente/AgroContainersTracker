using AgroContainerTracker.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgroContainerTracker.Data.Configurations
{
    public class ProductEntryCustomerConfiguration : IEntityTypeConfiguration<ProductEntrySellerEntity>
    {
        public void Configure(EntityTypeBuilder<ProductEntrySellerEntity> entityBuilder)
        {
            entityBuilder.ToTable("ProductEntriesCustomer");

            entityBuilder.HasKey(e => new { e.CampaingId, e.ProductEntryNumber, e.CustomerId });

            entityBuilder.Property(e => e.ProductEntryNumber).HasColumnType("int(11)");
            entityBuilder.Property(e => e.CampaingId).HasColumnType("int(11)");
            entityBuilder.Property(e => e.CustomerId).HasColumnType("int(11)");

            entityBuilder.HasOne(d => d.ProductEntry)
                .WithMany(p => p.Sellers)
                .HasForeignKey(d => new { d.CampaingId, d.ProductEntryNumber })
                .OnDelete(DeleteBehavior.Cascade);

            entityBuilder.HasOne(d => d.Customer)
                .WithMany(p => p.SellerProductEntries)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
