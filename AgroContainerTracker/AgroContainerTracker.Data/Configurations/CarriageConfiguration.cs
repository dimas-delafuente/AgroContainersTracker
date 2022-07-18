using AgroContainerTracker.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgroContainerTracker.Data.Configurations
{
    public class CarriageConfiguration : IEntityTypeConfiguration<Carriage>
    {
        public void Configure(EntityTypeBuilder<Carriage> entityBuilder)
        {
            entityBuilder.ToTable("Carriages");

            entityBuilder.HasKey(e => e.Id);

            entityBuilder.HasAlternateKey("LicenseNumber", "CompanyId");

            entityBuilder.Property(e => e.Id)
                .HasColumnName("CarriageId")
                .HasColumnType("int")
                .IsRequired();

            entityBuilder.Property<int>("CompanyId")
                .IsRequired();

            entityBuilder.Property(e => e.LicenseNumber)
                .HasColumnType("nvarchar(10)")
                .IsRequired();

            entityBuilder.HasOne(e => e.Carrier)
                .WithMany()
                .HasForeignKey("CompanyId")
                .IsRequired();
        }
    }
}
