using AgroContainerTracker.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgroContainerTracker.Data.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<CustomerEntity>
    {
        public void Configure(EntityTypeBuilder<CustomerEntity> entityBuilder)
        {
            entityBuilder.HasKey(e => e.CustomerId)
                .HasName("PRIMARY");

            entityBuilder.HasAlternateKey(e => e.CustomerNumber);

            entityBuilder.HasIndex(e => e.CountryId);

            entityBuilder.HasIndex(e => e.RateId);

            entityBuilder.Property(e => e.CustomerId).HasColumnType("int(11)");

            entityBuilder.Property(e => e.Address)
                .HasColumnType("longtext")
                .HasCharSet("utf8mb4")
                .HasCollation("utf8mb4_general_ci");

            entityBuilder.Property(e => e.BankAccount)
                .HasColumnType("longtext")
                .HasCharSet("utf8mb4")
                .HasCollation("utf8mb4_general_ci");

            entityBuilder.Property(e => e.CompanyCode)
                .IsRequired()
                .HasColumnType("longtext")
                .HasCharSet("utf8mb4")
                .HasCollation("utf8mb4_general_ci");

            entityBuilder.Property(e => e.CustomerNumber)
                .HasColumnType("int(11)");

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

            entityBuilder.Property(e => e.RateId).HasColumnType("int(11)");

            entityBuilder.Property(e => e.State)
                .HasColumnType("longtext")
                .HasCharSet("utf8mb4")
                .HasCollation("utf8mb4_general_ci");

            entityBuilder.HasOne(d => d.Country)
                .WithMany(p => p.Customers)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.NoAction);

            entityBuilder.HasOne(d => d.Rate)
                .WithMany(p => p.Customers)
                .HasForeignKey(d => d.RateId)
                .OnDelete(DeleteBehavior.ClientSetNull);

        }
    }
}