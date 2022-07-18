using AgroContainerTracker.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgroContainerTracker.Data.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> entityBuilder)
        {
            entityBuilder.ToTable("Customers");

            entityBuilder.HasKey("CompanyId");
            entityBuilder.Property<int>("CompanyId").IsRequired();

            entityBuilder.HasOne(d => d.Company)
                .WithOne();

            entityBuilder.OwnsOne(e => e.BillingInfo);

            entityBuilder.HasOne(d => d.Rate)
                .WithMany()
                .HasForeignKey("RateId")
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}