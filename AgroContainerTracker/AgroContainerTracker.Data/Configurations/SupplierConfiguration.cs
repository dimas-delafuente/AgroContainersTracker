using AgroContainerTracker.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgroContainerTracker.Data.Configurations
{
    public class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> entityBuilder)
        {
            entityBuilder.ToTable("Suppliers");

            entityBuilder.HasKey("CompanyId");
            entityBuilder.Property<int>("CompanyId").IsRequired();

            entityBuilder.HasOne(d => d.Company)
                .WithOne();
        }
    }
}