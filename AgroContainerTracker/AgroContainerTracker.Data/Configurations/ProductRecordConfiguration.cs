using AgroContainerTracker.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgroContainerTracker.Data.Configurations
{
    public class ProductRecordConfiguration : IEntityTypeConfiguration<ProductRecord>
    {
        public void Configure(EntityTypeBuilder<ProductRecord> entityBuilder)
        {
            entityBuilder.ToTable("ProductRecords");

            entityBuilder.HasKey("Id", "WeighingId", "CampaignId", "InputId");
            entityBuilder.Property<int>("CampaignId").IsRequired();
            entityBuilder.Property<int>("InputId").IsRequired();
            entityBuilder.Property<int>("WeighingId").IsRequired();

            entityBuilder.Property(e => e.Id)
                .HasColumnName("ProductRecordId")
                .HasColumnType("int");

            entityBuilder.HasOne(e => e.Weighing)
                .WithMany(d => d.ProductRecords)
                .HasForeignKey("WeighingId", "CampaignId", "InputId");

            entityBuilder.HasOne(d => d.Fruit)
                .WithMany()
                .HasForeignKey("FruitId")
                .OnDelete(DeleteBehavior.Restrict);

            entityBuilder.HasOne(d => d.Storage)
               .WithMany()
               .HasForeignKey("StorageId")
               .OnDelete(DeleteBehavior.Restrict);

            entityBuilder.HasOne(d => d.Packaging)
               .WithMany()
               .HasForeignKey("PackagingId")
               .OnDelete(DeleteBehavior.Restrict);

            entityBuilder.Property(e => e.IsOwnPackaging)
                .HasColumnType("bit");

            entityBuilder.Property(e => e.TotalDaysStored)
                .HasColumnType("int");

            entityBuilder.HasOne(e => e.Output)
                .WithMany()
                .HasForeignKey("OutputId");

            entityBuilder.HasOne(d => d.Seller)
               .WithMany()
               .HasForeignKey("CompanyId")
               .HasConstraintName("SellerId")
               .OnDelete(DeleteBehavior.Restrict);

            entityBuilder.HasOne(d => d.Buyer)
               .WithMany()
               .HasForeignKey("CompanyId")
               .HasConstraintName("BuyerId")
               .OnDelete(DeleteBehavior.Restrict);

            entityBuilder.OwnsOne(e => e.GrossWeight)
                .Property(p => p.Amount)
                    .HasConversion(
                        v => v.Value,
                        v => Amount.FromScalar(v)
                    )
                    .HasColumnType("decimal(7,5)");

            entityBuilder.OwnsOne(e => e.GrossWeight)
                .Property(p => p.Unit)
                    .HasConversion(
                        v => v.Name,
                        v => Enumeration.FromDisplayName<WeightUnit>(v)
                    )
                    .HasColumnType("varchar(5)")
                    .HasDefaultValueSql("kg");

            entityBuilder.OwnsOne(e => e.NetWeight)
                .Property(p => p.Amount)
                    .HasConversion(
                        v => v.Value,
                        v => Amount.FromScalar(v)
                    )
                    .HasColumnType("decimal(7,5)")
                    .HasDefaultValueSql("0");

            entityBuilder.OwnsOne(e => e.NetWeight)
                .Property(p => p.Unit)
                    .HasConversion(
                        v => v.Name,
                        v => Enumeration.FromDisplayName<WeightUnit>(v)
                    )
                    .HasColumnType("varchar(5)")
                    .HasDefaultValueSql("kg");

            entityBuilder.OwnsOne(e => e.TareWeight)
                .Property(p => p.Amount)
                    .HasConversion(
                        v => v.Value,
                        v => Amount.FromScalar(v)
                    )
                    .HasColumnType("decimal(7,5)")
                    .HasDefaultValueSql("0");

            entityBuilder.OwnsOne(e => e.TareWeight)
                .Property(p => p.Unit)
                    .HasConversion(
                        v => v.Name,
                        v => Enumeration.FromDisplayName<WeightUnit>(v)
                    )
                    .HasColumnType("varchar(5)")
                    .HasDefaultValueSql("kg");
        }
    }
}
