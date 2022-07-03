using AgroContainerTracker.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgroContainerTracker.Data.Configurations
{
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> entityBuilder)
        {
            entityBuilder.ToTable("Countries");

            entityBuilder.HasKey(e => e.CountryId).HasName("Countries_PK");

            entityBuilder.Property(e => e.CountryId).HasColumnType("int");

            entityBuilder.Property(e => e.Name)
                .IsRequired()
                .HasColumnType("varchar(25)");
        }
    }
}