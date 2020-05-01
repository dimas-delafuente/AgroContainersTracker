using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using AgroContainerTracker.Data.Entities;

namespace AgroContainerTracker.Data.Contexts
{
    public partial class ApplicationContext : DbContext
    {
        public ApplicationContext()
        {
        }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CarrierEntity> Carriers { get; set; }
        public virtual DbSet<ContainerEntity> Containers { get; set; }
        public virtual DbSet<CountryEntity> Countries { get; set; }
        public virtual DbSet<CreditorEntity> Creditors { get; set; }
        public virtual DbSet<CustomerEntity> Customers { get; set; }
        public virtual DbSet<DriverEntity> Drivers { get; set; }
        public virtual DbSet<FruitEntity> Fruits { get; set; }
        public virtual DbSet<PalotEntity> Palots { get; set; }
        public virtual DbSet<RateEntity> RateEntity { get; set; }
        public virtual DbSet<SupplierEntity> Suppliers { get; set; }
        public virtual DbSet<VehicleEntity> Vehicles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CarrierEntity>(entity =>
            {
                entity.HasKey(e => e.CompanyId)
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.CountryId);

                entity.Property(e => e.CompanyId).HasColumnType("int(11)");

                entity.Property(e => e.Address)
                    .HasColumnType("longtext")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.CompanyCode)
                    .IsRequired()
                    .HasColumnType("longtext")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.CompanyNumber).HasColumnType("int(11)");

                entity.Property(e => e.ContactName)
                    .HasColumnType("longtext")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.CountryId).HasColumnType("int(11)");

                entity.Property(e => e.Description)
                    .HasColumnType("varchar(300)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Email)
                    .HasColumnType("longtext")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Locality)
                    .HasColumnType("longtext")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Mobile)
                    .HasColumnType("varchar(9)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("longtext")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Phone).HasColumnType("bigint(20)");

                entity.Property(e => e.PostalCode)
                    .HasColumnType("varchar(5)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.SanitaryRegistrationNumber)
                    .HasColumnType("longtext")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.State)
                    .HasColumnType("longtext")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Carriers)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<ContainerEntity>(entity =>
            {
                entity.HasKey(e => e.ContainerId)
                    .HasName("PRIMARY");

                entity.Property(e => e.ContainerId).HasColumnType("int(11)");
            });

            modelBuilder.Entity<CountryEntity>(entity =>
            {
                entity.HasKey(e => e.CountryId)
                    .HasName("PRIMARY");

                entity.Property(e => e.CountryId).HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("longtext")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");
            });

            modelBuilder.Entity<CreditorEntity>(entity =>
            {
                entity.HasKey(e => e.CompanyId)
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.CountryId);

                entity.Property(e => e.CompanyId).HasColumnType("int(11)");

                entity.Property(e => e.Address)
                    .HasColumnType("longtext")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.CompanyCode)
                    .IsRequired()
                    .HasColumnType("longtext")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.CompanyNumber).HasColumnType("int(11)");

                entity.Property(e => e.ContactName)
                    .HasColumnType("longtext")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.CountryId).HasColumnType("int(11)");

                entity.Property(e => e.Description)
                    .HasColumnType("varchar(300)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Email)
                    .HasColumnType("longtext")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Locality)
                    .HasColumnType("longtext")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Mobile)
                    .HasColumnType("varchar(9)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("longtext")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Phone).HasColumnType("bigint(20)");

                entity.Property(e => e.PostalCode)
                    .HasColumnType("varchar(5)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.State)
                    .HasColumnType("longtext")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Creditors)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<CustomerEntity>(entity =>
            {
                entity.HasKey(e => e.CompanyId)
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.CountryId);

                entity.HasIndex(e => e.RateId);

                entity.Property(e => e.CompanyId).HasColumnType("int(11)");

                entity.Property(e => e.Address)
                    .HasColumnType("longtext")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.BankAccount)
                    .HasColumnType("longtext")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.CompanyCode)
                    .IsRequired()
                    .HasColumnType("longtext")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.CompanyNumber).HasColumnType("int(11)");

                entity.Property(e => e.ContactName)
                    .HasColumnType("longtext")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.CountryId).HasColumnType("int(11)");

                entity.Property(e => e.Description)
                    .HasColumnType("varchar(300)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Email)
                    .HasColumnType("longtext")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Locality)
                    .HasColumnType("longtext")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Mobile)
                    .HasColumnType("varchar(9)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("longtext")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Phone).HasColumnType("bigint(20)");

                entity.Property(e => e.PostalCode)
                    .HasColumnType("varchar(5)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.RateId).HasColumnType("int(11)");

                entity.Property(e => e.State)
                    .HasColumnType("longtext")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Rate)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.RateId);
            });

            modelBuilder.Entity<DriverEntity>(entity =>
            {
                entity.HasKey(e => e.DriverId)
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.CarrierCompanyId);

                entity.Property(e => e.DriverId).HasColumnType("int(11)");

                entity.Property(e => e.CarrierCompanyId).HasColumnType("int(11)");

                entity.Property(e => e.CompanyId).HasColumnType("int(11)");

                entity.Property(e => e.IdentificationNumber)
                    .HasColumnType("longtext")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Name)
                    .HasColumnType("longtext")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.HasOne(d => d.CarrierCompany)
                    .WithMany(p => p.Drivers)
                    .HasForeignKey(d => d.CarrierCompanyId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<FruitEntity>(entity =>
            {
                entity.HasKey(e => e.FruitId)
                    .HasName("PRIMARY");

                entity.Property(e => e.FruitId).HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .HasColumnType("longtext")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");
            });

            modelBuilder.Entity<PalotEntity>(entity =>
            {
                entity.HasKey(e => e.PalotId)
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.BuyerId);

                entity.HasIndex(e => e.ContainerId);

                entity.HasIndex(e => e.FruitId);

                entity.HasIndex(e => e.SellerId);

                entity.Property(e => e.PalotId).HasColumnType("int(11)");

                entity.Property(e => e.ArrivalNumber)
                    .IsRequired()
                    .HasColumnType("longtext")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.BuyerId).HasColumnType("int(11)");

                entity.Property(e => e.ContainerId).HasColumnType("int(11)");

                entity.Property(e => e.FruitId).HasColumnType("int(11)");

                entity.Property(e => e.PalotCode)
                    .IsRequired()
                    .HasColumnType("longtext")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.SellerId).HasColumnType("int(11)");

                entity.Property(e => e.Type).HasColumnType("int(11)");

                entity.HasOne(d => d.Buyer)
                    .WithMany(p => p.PalotsBuyer)
                    .HasForeignKey(d => d.BuyerId);

                entity.HasOne(d => d.Container)
                    .WithMany(p => p.Palots)
                    .HasForeignKey(d => d.ContainerId);

                entity.HasOne(d => d.Fruit)
                    .WithMany(p => p.Palots)
                    .HasForeignKey(d => d.FruitId);

                entity.HasOne(d => d.Seller)
                    .WithMany(p => p.PalotsSeller)
                    .HasForeignKey(d => d.SellerId);
            });

            modelBuilder.Entity<RateEntity>(entity =>
            {
                entity.HasKey(e => e.RateId)
                    .HasName("PRIMARY");

                entity.Property(e => e.RateId).HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("longtext")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");
            });

            modelBuilder.Entity<SupplierEntity>(entity =>
            {
                entity.HasKey(e => e.CompanyId)
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.CountryId)
                    .HasName("FK_Suppliers_Countries_CountryId_idx");

                entity.Property(e => e.CompanyId).HasColumnType("int(11)");

                entity.Property(e => e.Address)
                    .HasColumnType("longtext")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.CompanyCode)
                    .IsRequired()
                    .HasColumnType("longtext")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.CompanyNumber).HasColumnType("int(11)");

                entity.Property(e => e.ContactName)
                    .HasColumnType("longtext")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.CountryId).HasColumnType("int(11)");

                entity.Property(e => e.Description)
                    .HasColumnType("varchar(300)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Email)
                    .HasColumnType("longtext")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Locality)
                    .HasColumnType("longtext")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Mobile)
                    .HasColumnType("varchar(9)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("longtext")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Phone).HasColumnType("bigint(20)");

                entity.Property(e => e.PostalCode)
                    .HasColumnType("varchar(5)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.State)
                    .HasColumnType("longtext")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Suppliers)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<VehicleEntity>(entity =>
            {
                entity.HasKey(e => e.VehicleId)
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.CarrierCompanyId);

                entity.Property(e => e.VehicleId).HasColumnType("int(11)");

                entity.Property(e => e.CarriageRegistrationNumber)
                    .HasColumnType("longtext")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.CarrierCompanyId).HasColumnType("int(11)");

                entity.Property(e => e.CompanyId).HasColumnType("int(11)");

                entity.Property(e => e.RegistrationNumber)
                    .HasColumnType("longtext")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.HasOne(d => d.CarrierCompany)
                    .WithMany(p => p.Vehicles)
                    .HasForeignKey(d => d.CarrierCompanyId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
