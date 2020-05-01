using AgroContainerTracker.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgroContainerTracker.Data.Configurations
{
    public class PalotConfiguration : IEntityTypeConfiguration<PalotEntity>
    {
        public void Configure(EntityTypeBuilder<PalotEntity> entityBuilder)
        {
            entityBuilder.HasKey(e => e.PalotId)
                    .HasName("PRIMARY");

            entityBuilder.HasIndex(e => e.BuyerId);

            entityBuilder.HasIndex(e => e.ContainerId);

            entityBuilder.HasIndex(e => e.FruitId);

            entityBuilder.HasIndex(e => e.SellerId);

            entityBuilder.Property(e => e.PalotId).HasColumnType("int(11)");

            entityBuilder.Property(e => e.ArrivalNumber)
                .IsRequired()
                .HasColumnType("longtext")
                .HasCharSet("utf8mb4")
                .HasCollation("utf8mb4_general_ci");

            entityBuilder.Property(e => e.BuyerId).HasColumnType("int(11)");

            entityBuilder.Property(e => e.ContainerId).HasColumnType("int(11)");

            entityBuilder.Property(e => e.FruitId).HasColumnType("int(11)");

            entityBuilder.Property(e => e.PalotCode)
                .IsRequired()
                .HasColumnType("longtext")
                .HasCharSet("utf8mb4")
                .HasCollation("utf8mb4_general_ci");

            entityBuilder.Property(e => e.SellerId).HasColumnType("int(11)");

            entityBuilder.Property(e => e.Type).HasColumnType("int(11)");

            entityBuilder.HasOne(d => d.Buyer)
                .WithMany(p => p.PalotsBuyer)
                .HasForeignKey(d => d.BuyerId);

            entityBuilder.HasOne(d => d.Container)
                .WithMany(p => p.Palots)
                .HasForeignKey(d => d.ContainerId);

            entityBuilder.HasOne(d => d.Fruit)
                .WithMany(p => p.Palots)
                .HasForeignKey(d => d.FruitId);

            entityBuilder.HasOne(d => d.Seller)
                .WithMany(p => p.PalotsSeller)
                .HasForeignKey(d => d.SellerId);
        }
    }
}