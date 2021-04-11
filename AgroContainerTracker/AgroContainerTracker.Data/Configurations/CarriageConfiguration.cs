using AgroContainerTracker.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgroContainerTracker.Data.Configurations
{
    public class CarriageConfiguration : IEntityTypeConfiguration<CarriageEntity>
    {
        public void Configure(EntityTypeBuilder<CarriageEntity> entityBuilder)
        {
            entityBuilder.ToTable("Carriages");

            entityBuilder.HasKey(e => e.CarriageId).HasName("Carriages_PK");

            entityBuilder.Property(e => e.CarriageId).HasColumnType("int");

            entityBuilder.Property(e => e.CarrierId).HasColumnType("int");

            entityBuilder.Property(e => e.CarriageRegistrationNumber)
                .HasColumnType("varchar(10)");

            entityBuilder.HasOne(d => d.CarrierCompany)
                .WithMany(p => p.Carriages)
                .HasForeignKey(d => d.CarrierId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
