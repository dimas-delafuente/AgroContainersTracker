using AgroContainerTracker.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgroContainerTracker.Data.Configurations
{
    public class CarrierConfiguration : IEntityTypeConfiguration<Carrier>
    {
        public void Configure(EntityTypeBuilder<Carrier> entityBuilder)
        {
            entityBuilder.ToTable("Carriers");

            entityBuilder.HasKey("CompanyId");
            entityBuilder.Property<int>("CompanyId").IsRequired();

            entityBuilder.OwnsOne(e => e.SanitaryInfo);

            entityBuilder.HasOne(d => d.Company)
                .WithOne();

            entityBuilder.HasMany(d => d.Carriages)
                .WithOne(c => c.Carrier)
                .HasForeignKey("CompanyId")
                .OnDelete(DeleteBehavior.ClientCascade);

            entityBuilder.HasMany(d => d.Drivers)
                .WithOne(c => c.Carrier)
                .HasForeignKey("CompanyId")
                .OnDelete(DeleteBehavior.ClientCascade);

            entityBuilder.HasMany(d => d.Vehicles)
                .WithOne(c => c.Carrier)
                .HasForeignKey("CompanyId")
                .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}