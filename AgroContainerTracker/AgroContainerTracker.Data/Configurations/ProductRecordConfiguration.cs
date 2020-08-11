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

            entityBuilder.HasKey(e => e.ProductRecordId);

            entityBuilder.Property(e => e.ProductRecordId)
                .IsRequired()
                .HasColumnType("int(11)");

            entityBuilder.Property(e => e.ProductEntryNumber)
                .IsRequired()
                .HasColumnType("int(11)");
            entityBuilder.Property(e => e.CampaingId)
                .IsRequired()
                .HasColumnType("int(11)");
            entityBuilder.HasOne(d => d.ProductEntry)
                .WithMany(p => p.ProductRecords)
                .HasForeignKey(d => new { d.CampaingId, d.ProductEntryNumber })
                .OnDelete(DeleteBehavior.Cascade);

            entityBuilder.Property(e => e.ProductWeighingId)
                .IsRequired()
                .HasColumnType("int(11)");
            entityBuilder.HasOne(d => d.ProductWeighing)
                .WithMany(p => p.ProductRecords)
                .HasForeignKey(d => d.ProductWeighingId)
                .OnDelete(DeleteBehavior.Cascade);

            entityBuilder.Property(e => e.FruitId)
                .IsRequired(false)
                .HasColumnType("int(11)");
            entityBuilder.HasOne(d => d.Fruit)
                .WithMany(p => p.ProductRecords)
                .HasForeignKey(d => d.FruitId)
                .OnDelete(DeleteBehavior.SetNull);

            entityBuilder.Property(e => e.ColdRoomId)
                .IsRequired(false)
                .HasColumnType("int(11)");
            entityBuilder.HasOne(d => d.ColdRoom)
               .WithMany(p => p.ProductRecords)
               .HasForeignKey(d => d.ColdRoomId)
               .OnDelete(DeleteBehavior.SetNull);

            entityBuilder.Property(e => e.PackagingId)
                .IsRequired(false)
                .HasColumnType("int(11)");
            entityBuilder.HasOne(d => d.Packaging)
               .WithMany(p => p.ProductRecords)
               .HasForeignKey(d => d.PackagingId)
               .OnDelete(DeleteBehavior.SetNull);
            entityBuilder.Property(e => e.IsOwnPackaging)
                .HasColumnType("bit");

            entityBuilder.Property(e => e.TotalDaysStored)
                .HasColumnType("int(5)");
            entityBuilder.Property(e => e.ProductExitId)
                .IsRequired(false)
                .HasColumnType("int(11)");

            entityBuilder.Property(e => e.SellerId)
                .IsRequired(false)
                .HasColumnType("int(11)");
            entityBuilder.HasOne(d => d.Seller)
               .WithMany(p => p.SellerProductRecords)
               .HasForeignKey(d => d.SellerId)
               .OnDelete(DeleteBehavior.SetNull);

            entityBuilder.Property(e => e.BuyerId)
                .IsRequired(false)
                .HasColumnType("int(11)");
            entityBuilder.HasOne(d => d.Buyer)
               .WithMany(p => p.BuyerProductRecords)
               .HasForeignKey(d => d.BuyerId)
               .OnDelete(DeleteBehavior.SetNull);

            entityBuilder.Property(e => e.GrossWeight)
                .IsRequired()
                .HasColumnType("double");
            entityBuilder.Property(e => e.NetWeight)
                .IsRequired()
                .HasColumnType("double");
            entityBuilder.Property(e => e.TareWeight)
                .IsRequired()
                .HasColumnType("double"); 

        }
    }
}
