using AgroContainerTracker.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgroContainerTracker.Data.Configurations
{
    public class CampaignConfiguration : IEntityTypeConfiguration<Campaign>
    {
        public void Configure(EntityTypeBuilder<Campaign> entityBuilder)
        {
            entityBuilder.ToTable("Campaigns");

            entityBuilder.HasKey("CampaignId");

            entityBuilder.Property(e => e.Id)
                .HasColumnName("CampaignId")
                .IsRequired()
                .HasColumnType("int");

            entityBuilder.HasMany(e => e.Inputs)
                .WithOne(d => d.Campaign)
                .HasForeignKey("InputId", "CampaignId");
        }
    }
}
