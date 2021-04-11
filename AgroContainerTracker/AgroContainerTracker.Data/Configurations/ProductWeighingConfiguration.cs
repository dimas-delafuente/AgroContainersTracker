using AgroContainerTracker.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgroContainerTracker.Data.Configurations
{
    public class ProductWeighingConfiguration : IEntityTypeConfiguration<ProductWeighingEntity>
    {
        public void Configure(EntityTypeBuilder<ProductWeighingEntity> entityBuilder)
        {
            entityBuilder.ToTable("ProductWeighings");

            entityBuilder.HasKey(e => new { e.CampaingId, e.ProductEntryNumber, e.ProductWeighingId }).HasName("ProductWeighings_PK");

            entityBuilder.Property(e => e.CampaingId)   
                .HasColumnType("int");
  
            entityBuilder.Property(e => e.ProductEntryNumber)
                .IsRequired()
                .HasColumnType("int");
            entityBuilder.HasOne(d => d.ProductEntry)
                .WithMany(p => p.ProductWeighings)
                .HasForeignKey(d => new { d.CampaingId, d.ProductEntryNumber })
                .OnDelete(DeleteBehavior.Cascade);

            entityBuilder.Property(e => e.ProductWeighingId)
                .IsRequired()
                .HasColumnType("int");

            entityBuilder.Property(e => e.FruitId)
                .IsRequired(false)
                .HasColumnType("int");
            entityBuilder.HasOne(d => d.Fruit)
                .WithMany(p => p.ProductWeighings)
                .HasForeignKey(d => d.FruitId)
                .OnDelete(DeleteBehavior.SetNull);

            entityBuilder.Property(e => e.ColdRoomId)
                .IsRequired(false)
                .HasColumnType("int");
            entityBuilder.HasOne(d => d.ColdRoom)
               .WithMany(p => p.ProductWeighings)
               .HasForeignKey(d => d.ColdRoomId)
               .OnDelete(DeleteBehavior.Restrict);

            entityBuilder.Property(e => e.RateId)
                .IsRequired(false)
                .HasColumnType("int");
            entityBuilder.HasOne(d => d.Rate)
               .WithMany(p => p.ProductWeighings)
               .HasForeignKey(d => d.RateId)
               .OnDelete(DeleteBehavior.Restrict);

            entityBuilder.Property(e => e.SellerId)
                .IsRequired(false)
                .HasColumnType("int");
            entityBuilder.HasOne(d => d.Seller)
               .WithMany(p => p.SellerProductWeighings)
               .HasForeignKey(d => d.SellerId)
               .OnDelete(DeleteBehavior.Restrict);

            entityBuilder.Property(e => e.BuyerId)
                .IsRequired(false)
                .HasColumnType("int");
            entityBuilder.HasOne(d => d.Buyer)
               .WithMany(p => p.BuyerProductWeighings)
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

            entityBuilder.Property(e => e.ExitDate)
                .IsRequired(false)
                .HasColumnType("datetime");

            entityBuilder.Property(e => e.TotalLabels)
                .HasColumnType("int");    

        }
    }
}
