using AgroContainerTracker.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgroContainerTracker.Data.Configurations
{
    public class CarrierConfiguration : IEntityTypeConfiguration<CarrierEntity>
    {
        public void Configure(EntityTypeBuilder<CarrierEntity> entityBuilder)
        {
            entityBuilder.ToTable("Carriers");

            entityBuilder.HasKey(e => e.CarrierId).HasName("Carriers_PK");

            entityBuilder.HasIndex(e => e.CountryId);

            entityBuilder.Property(e => e.CarrierId).HasColumnType("int");

            entityBuilder.Property(e => e.Address)
                .HasColumnType("nvarchar(150)");

            entityBuilder.Property(e => e.CompanyCode)
                .IsRequired()
                .HasColumnType("varchar(15)");

            entityBuilder.Property(e => e.CarrierNumber).HasColumnType("int");

            entityBuilder.Property(e => e.ContactName)
                .HasColumnType("nvarchar(100)");

            entityBuilder.Property(e => e.CountryId).HasColumnType("int");

            entityBuilder.Property(e => e.Description)
                .HasColumnType("nvarchar(300)");

            entityBuilder.Property(e => e.Email)
                .HasColumnType("varchar(50)");

            entityBuilder.Property(e => e.Locality)
                .HasColumnType("nvarchar(75)");

            entityBuilder.Property(e => e.Mobile)
                .HasColumnType("varchar(9)");

            entityBuilder.Property(e => e.Name)
                .IsRequired()
                .HasColumnType("nvarchar(150)");

            entityBuilder.Property(e => e.Phone).HasColumnType("int");

            entityBuilder.Property(e => e.PostalCode)
                .HasColumnType("varchar(5)");

            entityBuilder.Property(e => e.SanitaryRegistrationNumber)
                .HasColumnType("varchar(15)");

            entityBuilder.Property(e => e.State)
                .HasColumnType("varchar(50)");

            entityBuilder.HasOne(d => d.Country)
                .WithMany(p => p.Carriers)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.NoAction);

            entityBuilder.HasMany(d => d.Carriages)
                .WithOne(p => p.CarrierCompany)
                .HasForeignKey(k => k.CarrierId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}