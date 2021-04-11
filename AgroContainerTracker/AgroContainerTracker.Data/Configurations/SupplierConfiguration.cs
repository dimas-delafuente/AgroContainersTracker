using AgroContainerTracker.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgroContainerTracker.Data.Configurations
{
    public class SupplierConfiguration : IEntityTypeConfiguration<SupplierEntity>
    {
        public void Configure(EntityTypeBuilder<SupplierEntity> entityBuilder)
        {
            entityBuilder.ToTable("Suppliers");

            entityBuilder.HasKey(e => e.SupplierId)
                    .HasName("Suppliers_PK");

            entityBuilder.HasIndex(e => e.CountryId)
                .HasName("FK_Suppliers_Countries_CountryId_idx");

            entityBuilder.Property(e => e.SupplierId).HasColumnType("int");

            entityBuilder.Property(e => e.Address)
                .HasColumnType("nvarchar(150)");

            entityBuilder.Property(e => e.CompanyCode)
                .IsRequired()
                .HasColumnType("varchar(15)");

            entityBuilder.Property(e => e.SupplierNumber).HasColumnType("int");

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

            entityBuilder.HasOne(d => d.Country)
                .WithMany(p => p.Suppliers)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}