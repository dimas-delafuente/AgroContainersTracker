using AgroContainerTracker.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgroContainerTracker.Data.Configurations
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> entityBuilder)
        {
            entityBuilder.ToTable("Companies");

            entityBuilder.HasKey(e => e.Id)
                .HasName("Companies_PK");

            entityBuilder.Property(e => e.Id)
                .HasColumnName("CompanyId")
                .HasColumnType("int")
                .IsRequired();

            entityBuilder.Property(e => e.CompanyCode)
                .IsRequired()
                .HasColumnType("varchar(15)");

            entityBuilder.Property(e => e.Name)
                .IsRequired()
                .HasColumnType("nvarchar(150)");

            entityBuilder.OwnsOne(e => e.Address)
                .Property(e => e.Country)
                .HasConversion(
                    v => v.Id,
                    v => Enumeration.FromValue<Country>(v));

            entityBuilder.Property(e => e.ContactName)
                .HasColumnType("nvarchar(100)");

            entityBuilder.Property(e => e.Description)
                .HasColumnType("nvarchar(300)");

            entityBuilder.Property(e => e.Email)
                .HasColumnType("varchar(50)");

            entityBuilder.Property(e => e.Mobile)
                .HasColumnType("nvarchar(15)");

            entityBuilder.Property(e => e.Phone)
                .HasColumnType("nvarchar(15)");
        }
    }
}