using AgroContainerTracker.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgroContainerTracker.Data.Configurations
{
    public class InputConfiguration : IEntityTypeConfiguration<Input>
    {
        public void Configure(EntityTypeBuilder<Input> entityBuilder)
        {
            entityBuilder.ToTable("Inputs");

            entityBuilder.HasKey("Id", "CampaignId");

            entityBuilder.Property<int>("CampaignId").IsRequired();

            entityBuilder.Property(e => e.Id)
                .HasColumnName("InputId")
                .HasColumnType("int")
                .UseIdentityColumn();

            entityBuilder.HasOne(e => e.Campaign)
                .WithMany(d => d.Inputs)
                .HasForeignKey("CampaignId")
                .IsRequired();

            entityBuilder.Property(e => e.CreatedDate)
                .HasColumnType("datetime")
                .IsRequired();

            entityBuilder.Property(e => e.Closed)
                .HasDefaultValue(0)
                .HasColumnType("bit");

            entityBuilder.Property(e => e.HasOutput)
                .HasDefaultValue(0)
                .HasColumnType("bit");

            entityBuilder.Property(e => e.Observations)
                .IsRequired(false)
                .HasColumnType("nvarchar(250)");

            entityBuilder.Property(e => e.HasHail)
                .HasColumnType("bit");
            entityBuilder.Property(e => e.HasPlague)
                .HasColumnType("bit");
            entityBuilder.Property(e => e.HasSecondPasses)
                .HasColumnType("bit");

            entityBuilder.HasOne(e => e.Buyer)
                .WithMany()
                .HasForeignKey("CompanyId")
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            entityBuilder.HasOne(e => e.Payer)
                .WithMany()
                .HasForeignKey("CompanyId")
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            entityBuilder.HasMany(e => e.Weighings)
                .WithOne(d => d.Input)
                .HasForeignKey("CampaignId", "InputId", "WeighingId");

            entityBuilder.HasMany(e => e.Sellers)
                .WithOne(d => d.Input)
                .HasForeignKey("CampaignId", "InputId", "CompanyId");
        }
    }

    public class InputSellerConfiguration : IEntityTypeConfiguration<InputSeller>
    {
        public void Configure(EntityTypeBuilder<InputSeller> entityBuilder)
        {
            entityBuilder.ToTable("InputSellers");

            entityBuilder.HasKey("CampaignId", "InputId", "CompanyId");

            entityBuilder.Property<int>("CampaignId").IsRequired();
            entityBuilder.Property<int>("InputId").IsRequired();
            entityBuilder.Property<int>("CompanyId").IsRequired();

            entityBuilder.HasOne(e => e.Input)
                .WithMany(d => d.Sellers)
                .HasForeignKey("InputId", "CampaignId");

            // entityBuilder.HasOne(d => d.Seller)
            //     .WithMany()
            //     .HasForeignKey("CompanyId")
            //     .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
