using AgroContainerTracker.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgroContainerTracker.Data.Configurations
{
    public class ContainerConfiguration : IEntityTypeConfiguration<ContainerEntity>
    {
        public void Configure(EntityTypeBuilder<ContainerEntity> entityBuilder)
        {
            entityBuilder.ToTable("Containers");

            entityBuilder.HasKey(e => e.ContainerId)
                    .HasName("PRIMARY");

            entityBuilder.Property(e => e.ContainerId).HasColumnType("int(11)");

            entityBuilder.Property(e => e.Size).HasColumnType("double");

            entityBuilder.Property(e => e.Temperature).HasColumnType("double");


        }
    }
}