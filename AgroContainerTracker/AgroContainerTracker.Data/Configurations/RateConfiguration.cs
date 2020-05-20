using AgroContainerTracker.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgroContainerTracker.Data.Configurations
{
    public class RateConfiguration : IEntityTypeConfiguration<RateEntity>
    {
        public void Configure(EntityTypeBuilder<RateEntity> entityBuilder)
        {
            entityBuilder.ToTable("Rates");


            entityBuilder.HasKey(e => e.RateId)
                    .HasName("PRIMARY");

            entityBuilder.Property(e => e.RateId).HasColumnType("int(11)");

            entityBuilder.Property(e => e.Name)
                .IsRequired()
                .HasColumnType("longtext")
                .HasCharSet("utf8mb4")
                .HasCollation("utf8mb4_general_ci");

            entityBuilder.Property(e => e.Value)
                .HasColumnType("decimal(7,5)")
                .HasDefaultValue(0.00);

            entityBuilder.Property(e => e.SecondaryValue)
                .HasColumnType("decimal(7,5)")
                .HasDefaultValue(0.00);

            entityBuilder.Property(e => e.Description)
                .HasColumnType("varchar(300)")
                .HasCharSet("utf8mb4")
                .HasCollation("utf8mb4_general_ci");

        }
    }
}