using AgroContainerTracker.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgroContainerTracker.Data.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<CustomerEntity>
    {
        public void Configure(EntityTypeBuilder<CustomerEntity> entityBuilder)
        {
            entityBuilder.ToTable("Customers");


            entityBuilder.HasKey(e => e.CustomerId)
                .HasName("Customers_PK");

            entityBuilder.Property(e => e.CustomerId).HasColumnType("int");

            entityBuilder.Property(e => e.Address)
                .HasColumnType("nvarchar(150)");

            entityBuilder.Property(e => e.CompanyCode)
                .IsRequired()
                .HasColumnType("varchar(15)");

            entityBuilder.Property(e => e.BankAccount)
                .HasColumnType("varchar(50)");

            entityBuilder.Property(e => e.CustomerNumber)
                .HasColumnType("int");

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

            entityBuilder.Property(e => e.State)
                .HasColumnType("nvarchar(50)");

            entityBuilder.Property(e => e.RateId).HasColumnType("int");

            entityBuilder.HasOne(d => d.Country)
                .WithMany(p => p.Customers)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.NoAction);

            entityBuilder.HasOne(d => d.Rate)
                .WithMany(p => p.Customers)
                .HasForeignKey(d => d.RateId)
                .OnDelete(DeleteBehavior.SetNull);

            entityBuilder.HasMany(d => d.SellerProductEntries)
                .WithOne(p => p.Customer)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}