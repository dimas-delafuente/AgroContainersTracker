using AgroContainerTracker.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgroContainerTracker.Data.Configurations
{
    public class RateConfiguration : IEntityTypeConfiguration<Rate>
    {
        public void Configure(EntityTypeBuilder<Rate> entityBuilder)
        {
            entityBuilder.ToTable("Rates");

            entityBuilder.HasKey(e => e.Id)
                .HasName("Rates_PK");

            entityBuilder.Property(e => e.Id)
                .HasColumnName("RateId")
                .HasColumnType("int");

            entityBuilder.Property(e => e.Name)
                .IsRequired()
                .HasColumnType("nvarchar(25)");

            entityBuilder.OwnsOne(e => e.MainPrice)
                .Property(p => p.Amount)
                    .HasConversion(
                        v => v.Value,
                        v => Amount.FromScalar(v)
                    )
                    .HasColumnType("decimal(5,7)");

            entityBuilder.OwnsOne(e => e.MainPrice)
                .Property(p => p.Currency)
                    .HasConversion(
                        v => v.Name,
                        v => Enumeration.FromDisplayName<Currency>(v)
                    )
                    .HasColumnType("varchar(5)")
                    .HasDefaultValueSql("EUR");

            entityBuilder.OwnsOne(e => e.SecondaryPrice)
                .Property(p => p.Amount)
                    .HasConversion(
                        v => v.Value,
                        v => Amount.FromScalar(v)
                    )
                    .HasColumnType("decimal(5,7)");

            entityBuilder.OwnsOne(e => e.SecondaryPrice)
                .Property(p => p.Currency)
                    .HasConversion(
                        v => v.Name,
                        v => Enumeration.FromDisplayName<Currency>(v)
                    )
                    .HasColumnType("varchar(5)")
                    .HasDefaultValueSql("EUR");

            entityBuilder.Property(e => e.Description)
                .HasColumnType("nvarchar(100)");
        }
    }
}