using AgroContainerTracker.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgroContainerTracker.Data.Configurations
{
    public class CreditorConfiguration : IEntityTypeConfiguration<CreditorEntity>
    {
        public void Configure(EntityTypeBuilder<CreditorEntity> entityBuilder)
        {
            entityBuilder.ToTable("Creditors");

            entityBuilder.HasKey(e => e.CreditorId)
                    .HasName("Creditors_PK");

            entityBuilder.HasIndex(e => e.CountryId);

            entityBuilder.Property(e => e.CreditorId).HasColumnType("int");

            entityBuilder.Property(e => e.Address)
                .HasColumnType("nvarchar(150)");
            entityBuilder.Property(e => e.CompanyCode)
                .IsRequired()
                .HasColumnType("varchar(15)");

            entityBuilder.Property(e => e.CreditorNumber).HasColumnType("int");

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
                .WithMany(p => p.Creditors)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}