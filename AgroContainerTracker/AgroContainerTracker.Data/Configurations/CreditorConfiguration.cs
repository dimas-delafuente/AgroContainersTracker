using AgroContainerTracker.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgroContainerTracker.Data.Configurations
{
    public class CreditorConfiguration : IEntityTypeConfiguration<Creditor>
    {
        public void Configure(EntityTypeBuilder<Creditor> entityBuilder)
        {
            entityBuilder.ToTable("Creditors");

            entityBuilder.HasKey("CompanyId");
            entityBuilder.Property<int>("CompanyId").IsRequired();

            entityBuilder.HasOne(d => d.Company)
                .WithOne();
        }
    }
}