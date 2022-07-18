using AgroContainerTracker.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgroContainerTracker.Data.Configurations
{
    public class WeighingConfiguration : IEntityTypeConfiguration<Weighing>
    {
        public void Configure(EntityTypeBuilder<Weighing> entityBuilder)
        {
            entityBuilder.ToTable("Weighings");

            entityBuilder.HasKey("Id", "CampaignId", "InputId");

            entityBuilder.Property<int>("InputId").IsRequired();
            entityBuilder.Property<int>("CampaignId").IsRequired();

            entityBuilder.Property(e => e.Id)
                .IsRequired()
                .HasColumnType("int")
                .HasColumnName("WeighingId");

            entityBuilder.HasOne(e => e.Input)
                .WithMany(d => d.Weighings)
                .HasForeignKey("InputId", "CampaignId");

            entityBuilder.HasOne(d => d.Fruit)
                .WithMany()
                .HasForeignKey("FruitId")
                .OnDelete(DeleteBehavior.SetNull);

            entityBuilder.HasOne(d => d.Storage)
               .WithMany()
               .HasForeignKey("StorageId")
               .OnDelete(DeleteBehavior.Restrict);

            entityBuilder.HasOne(d => d.Rate)
               .WithMany()
               .HasForeignKey("RateId")
               .OnDelete(DeleteBehavior.Restrict);

            entityBuilder.HasOne(d => d.Seller)
               .WithMany()
               .HasForeignKey("CustomerId")
               .HasConstraintName("SellerId")
               .OnDelete(DeleteBehavior.Restrict);

            entityBuilder.HasOne(d => d.Buyer)
               .WithMany()
               .HasForeignKey("CustomerId")
               .HasConstraintName("BuyerId")
               .OnDelete(DeleteBehavior.Restrict);

            entityBuilder.Ignore(e => e.GrossWeight);

            entityBuilder.Ignore(e => e.NetWeight);

            entityBuilder.Ignore(e => e.TareWeight);

            entityBuilder.Property(e => e.OutputDate)
                .IsRequired(false)
                .HasColumnType("datetime");

            entityBuilder.Property(e => e.TotalLabels)
                .HasColumnType("int");

            entityBuilder.HasMany(e => e.ProductRecords)
                .WithOne(d => d.Weighing)
                .HasForeignKey("ProductRecordId", "WeighingId", "CampaignId", "InputId");
        }
    }
}
