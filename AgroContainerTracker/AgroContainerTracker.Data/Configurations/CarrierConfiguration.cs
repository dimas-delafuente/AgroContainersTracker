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

            entityBuilder.HasKey(e => e.CarrierId)
                    .HasName("PRIMARY");

            entityBuilder.HasIndex(e => e.CountryId);


            entityBuilder.Property(e => e.CarrierId).HasColumnType("int(11)");

            entityBuilder.Property(e => e.Address)
                .HasColumnType("longtext")
                .HasCharSet("utf8mb4")
                .HasCollation("utf8mb4_general_ci");

            entityBuilder.Property(e => e.CompanyCode)
                .IsRequired()
                .HasColumnType("longtext")
                .HasCharSet("utf8mb4")
                .HasCollation("utf8mb4_general_ci");

            entityBuilder.Property(e => e.CarrierNumber).HasColumnType("int(11)");

            entityBuilder.Property(e => e.ContactName)
                .HasColumnType("longtext")
                .HasCharSet("utf8mb4")
                .HasCollation("utf8mb4_general_ci");

            entityBuilder.Property(e => e.CountryId).HasColumnType("int(11)");

            entityBuilder.Property(e => e.Description)
                .HasColumnType("varchar(300)")
                .HasCharSet("utf8mb4")
                .HasCollation("utf8mb4_general_ci");

            entityBuilder.Property(e => e.Email)
                .HasColumnType("longtext")
                .HasCharSet("utf8mb4")
                .HasCollation("utf8mb4_general_ci");

            entityBuilder.Property(e => e.Locality)
                .HasColumnType("longtext")
                .HasCharSet("utf8mb4")
                .HasCollation("utf8mb4_general_ci");

            entityBuilder.Property(e => e.Mobile)
                .HasColumnType("varchar(9)")
                .HasCharSet("utf8mb4")
                .HasCollation("utf8mb4_general_ci");

            entityBuilder.Property(e => e.Name)
                .IsRequired()
                .HasColumnType("longtext")
                .HasCharSet("utf8mb4")
                .HasCollation("utf8mb4_general_ci");

            entityBuilder.Property(e => e.Phone).HasColumnType("bigint(20)");

            entityBuilder.Property(e => e.PostalCode)
                .HasColumnType("varchar(5)")
                .HasCharSet("utf8mb4")
                .HasCollation("utf8mb4_general_ci");

            entityBuilder.Property(e => e.SanitaryRegistrationNumber)
                .HasColumnType("longtext")
                .HasCharSet("utf8mb4")
                .HasCollation("utf8mb4_general_ci");

            entityBuilder.Property(e => e.State)
                .HasColumnType("longtext")
                .HasCharSet("utf8mb4")
                .HasCollation("utf8mb4_general_ci");

            entityBuilder.HasOne(d => d.Country)
                .WithMany(p => p.Carriers)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}