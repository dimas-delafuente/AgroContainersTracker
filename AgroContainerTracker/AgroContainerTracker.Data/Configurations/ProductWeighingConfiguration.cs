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

            entityBuilder.HasKey(e => e.ProductWeighingId);

            entityBuilder.Property(e => e.ProductWeighingId)
                .IsRequired()
                .HasColumnType("int(11)");
            

            entityBuilder.Property(e => e.ProductEntryNumber)
                .IsRequired()
                .HasColumnType("int(11)");
            entityBuilder.Property(e => e.CampaingId)
                .IsRequired()
                .HasColumnType("int(11)");
            entityBuilder.HasOne(d => d.ProductEntry)
                .WithMany(p => p.ProductWeighings)
                .HasForeignKey(d => new { d.CampaingId, d.ProductEntryNumber })
                .OnDelete(DeleteBehavior.Cascade);

            entityBuilder.Property(e => e.FruitId)
                .IsRequired(false)
                .HasColumnType("int(11)");
            entityBuilder.HasOne(d => d.Fruit)
                .WithMany(p => p.ProductWeighings)
                .HasForeignKey(d => d.FruitId)
                .OnDelete(DeleteBehavior.SetNull);

            entityBuilder.Property(e => e.ColdRoomId)
                .IsRequired(false)
                .HasColumnType("int(11)");
            entityBuilder.HasOne(d => d.ColdRoom)
               .WithMany(p => p.ProductWeighings)
               .HasForeignKey(d => d.ColdRoomId)
               .OnDelete(DeleteBehavior.SetNull);

            entityBuilder.Property(e => e.RateId)
                .IsRequired(false)
                .HasColumnType("int(11)");
            entityBuilder.HasOne(d => d.Rate)
               .WithMany(p => p.ProductWeighings)
               .HasForeignKey(d => d.RateId)
               .OnDelete(DeleteBehavior.SetNull);

            entityBuilder.Property(e => e.SellerId)
                .IsRequired(false)
                .HasColumnType("int(11)");
            entityBuilder.HasOne(d => d.Seller)
               .WithMany(p => p.SellerProductWeighings)
               .HasForeignKey(d => d.SellerId)
               .OnDelete(DeleteBehavior.SetNull);

            entityBuilder.Property(e => e.BuyerId)
                .IsRequired(false)
                .HasColumnType("int(11)");
            entityBuilder.HasOne(d => d.Buyer)
               .WithMany(p => p.BuyerProductWeighings)
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

            entityBuilder.Property(e => e.ExitDate)
                .IsRequired(false)
                .HasColumnType("datetime");

            entityBuilder.Property(e => e.TotalLabels)
                .HasColumnType("int(5)");    

        }
    }
}
