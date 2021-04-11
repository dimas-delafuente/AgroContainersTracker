using AgroContainerTracker.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgroContainerTracker.Data.Configurations
{
    public class ProductRecordConfiguration : IEntityTypeConfiguration<ProductRecordEntity>
    {
        public void Configure(EntityTypeBuilder<ProductRecordEntity> entityBuilder)
        {
            entityBuilder.ToTable("ProductRecords");

            entityBuilder.HasKey(e => e.ProductRecordId).HasName("ProductRecords_PK");

            entityBuilder.Property(e => e.CampaingId)
                .HasColumnType("int");

            entityBuilder.Property(e => e.ProductEntryNumber)
                .IsRequired()
                .HasColumnType("int");

            entityBuilder.Property(e => e.ProductWeighingId)
                .IsRequired()
                .HasColumnType("int");
            entityBuilder.HasOne(d => d.ProductWeighing)
                .WithMany(p => p.ProductRecords)
                .HasForeignKey(d => new { d.CampaingId, d.ProductEntryNumber, d.ProductWeighingId })
                .OnDelete(DeleteBehavior.Cascade);
            
            entityBuilder.Property(e => e.ProductRecordId)
                .IsRequired()
                .HasColumnType("int");

            entityBuilder.Property(e => e.FruitId)
                .IsRequired(false)
                .HasColumnType("int");
            entityBuilder.HasOne(d => d.Fruit)
                .WithMany(p => p.ProductRecords)
                .HasForeignKey(d => d.FruitId)
                .OnDelete(DeleteBehavior.SetNull);

            entityBuilder.Property(e => e.ColdRoomId)
                .IsRequired(false)
                .HasColumnType("int");
            entityBuilder.HasOne(d => d.ColdRoom)
               .WithMany(p => p.ProductRecords)
               .HasForeignKey(d => d.ColdRoomId)
               .OnDelete(DeleteBehavior.Restrict);

            entityBuilder.Property(e => e.PackagingId)
                .IsRequired(false)
                .HasColumnType("int");
            entityBuilder.HasOne(d => d.Packaging)
               .WithMany(p => p.ProductRecords)
               .HasForeignKey(d => d.PackagingId)
               .OnDelete(DeleteBehavior.Restrict);
            entityBuilder.Property(e => e.IsOwnPackaging)
                .HasColumnType("bit");

            entityBuilder.Property(e => e.TotalDaysStored)
                .HasColumnType("int");
            entityBuilder.Property(e => e.ProductExitId)
                .IsRequired(false)
                .HasColumnType("int");

            entityBuilder.Property(e => e.SellerId)
                .IsRequired(false)
                .HasColumnType("int");
            entityBuilder.HasOne(d => d.Seller)
               .WithMany(p => p.SellerProductRecords)
               .HasForeignKey(d => d.SellerId)
               .OnDelete(DeleteBehavior.Restrict);

            entityBuilder.Property(e => e.BuyerId)
                .IsRequired(false)
                .HasColumnType("int");
            entityBuilder.HasOne(d => d.Buyer)
               .WithMany(p => p.BuyerProductRecords)
               .HasForeignKey(d => d.BuyerId)
               .OnDelete(DeleteBehavior.Restrict);

            entityBuilder.Property(e => e.GrossWeight)
                .IsRequired()
                .HasColumnType("float");
            entityBuilder.Property(e => e.NetWeight)
                .IsRequired()
                .HasColumnType("float");
            entityBuilder.Property(e => e.TareWeight)
                .IsRequired()
                .HasColumnType("float"); 

        }
    }
}
