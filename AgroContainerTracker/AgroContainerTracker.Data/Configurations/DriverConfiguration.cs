using AgroContainerTracker.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgroContainerTracker.Data.Configurations
{
    public class DriverConfiguration : IEntityTypeConfiguration<Driver>
    {
        public void Configure(EntityTypeBuilder<Driver> entityBuilder)
        {
            entityBuilder.ToTable("Drivers");

            entityBuilder.HasKey(e => e.Id);
            entityBuilder.HasAlternateKey("IdentificationNumber", "CompanyId");

            entityBuilder.Property(e => e.Id)
                .HasColumnName("DriverId")
                .HasColumnType("int")
                .IsRequired();

            entityBuilder.Property<int>("CompanyId")
                .IsRequired();

            entityBuilder.Property(e => e.IdentificationNumber)
                .HasColumnType("nvarchar(12)")
                .IsRequired();

            entityBuilder.Property(e => e.Name)
                .HasColumnType("varchar(100)")
                .IsRequired();

            entityBuilder.HasOne(d => d.Carrier)
                .WithMany()
                .HasForeignKey("CompanyId")
                .IsRequired();
        }
    }
}