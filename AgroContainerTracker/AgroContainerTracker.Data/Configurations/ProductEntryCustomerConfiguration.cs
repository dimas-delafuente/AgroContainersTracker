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

            entityBuilder.HasKey(e => new { e.CampaingId, e.ProductEntryNumber, e.CustomerId }).HasName("ProductEntriesCustomer_PK");

            entityBuilder.Property(e => e.ProductEntryNumber).HasColumnType("int");
            entityBuilder.Property(e => e.CampaingId).HasColumnType("int");
            entityBuilder.Property(e => e.CustomerId).HasColumnType("int");
        }
    }
}
