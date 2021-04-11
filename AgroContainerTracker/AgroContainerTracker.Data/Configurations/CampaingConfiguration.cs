using AgroContainerTracker.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgroContainerTracker.Data.Configurations
{
    public class CampaingConfiguration : IEntityTypeConfiguration<CampaingEntity>
    {
        public void Configure(EntityTypeBuilder<CampaingEntity> entityBuilder)
        {
            entityBuilder.ToTable("Campaings");

            entityBuilder.HasKey(e => e.CampaingId).HasName("Campaings_PK");

            entityBuilder.Property(e => e.CampaingId)
                .IsRequired()
                .HasColumnType("int");

        }
    }
}
