using AgroContainerTracker.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgroContainerTracker.Data.Configurations
{
    public class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
    {
        public void Configure(EntityTypeBuilder<Vehicle> entityBuilder)
        {
            entityBuilder.ToTable("Vehicles");

            entityBuilder.HasKey(e => e.Id);

            entityBuilder.HasAlternateKey("LicenseNumber", "CompanyId");

            entityBuilder.Property(e => e.Id)
                .HasColumnName("VehicleId")
                .HasColumnType("int")
                .IsRequired();

            entityBuilder.Property<int>("CompanyId")
                .IsRequired();

            entityBuilder.Property(e => e.LicenseNumber)
                .HasColumnType("nvarchar(10)")
                .IsRequired();

            entityBuilder.HasOne(d => d.Carrier)
                .WithMany()
                .HasForeignKey("CompanyId")
                .IsRequired();
        }
    }
}