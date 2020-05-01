using AgroContainerTracker.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgroContainerTracker.Data.Configurations
{
    public class CountryConfiguration : IEntityTypeConfiguration<CountryEntity>
    {
        public void Configure(EntityTypeBuilder<CountryEntity> entityBuilder)
        {
            entityBuilder.HasKey(e => e.CountryId)
                    .HasName("PRIMARY");

            entityBuilder.Property(e => e.CountryId).HasColumnType("int(11)");

            entityBuilder.Property(e => e.Name)
                .IsRequired()
                .HasColumnType("longtext")
                .HasCharSet("utf8mb4")
                .HasCollation("utf8mb4_general_ci");
        }
    }
}