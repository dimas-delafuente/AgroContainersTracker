using AgroContainerTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgroContainerTracker.Data.Configurations
{
    public class RateConfiguration : IEntityTypeConfiguration<Rate>
    {
        public void Configure(EntityTypeBuilder<Rate> entityBuilder)
        {
            entityBuilder.ToTable("Rates");


            entityBuilder.HasKey(e => e.RateId)
                    .HasName("Rates_PK");

            entityBuilder.Property(e => e.RateId).HasColumnType("int");

            entityBuilder.Property(e => e.Name)
                .IsRequired()
                .HasColumnType("nvarchar(25)");

            entityBuilder.Property(e => e.Value)
                .HasColumnType("decimal(7,5)")
                .HasDefaultValue(0.00);

            entityBuilder.Property(e => e.SecondaryValue)
                .HasColumnType("decimal(7,5)")
                .HasDefaultValue(0.00);

            entityBuilder.Property(e => e.Description)
                .HasColumnType("nvarchar(100)");

        }
    }
}