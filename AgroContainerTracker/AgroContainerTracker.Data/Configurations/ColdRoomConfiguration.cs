using AgroContainerTracker.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgroContainerTracker.Data.Configurations
{
    public class ContainerConfiguration : IEntityTypeConfiguration<ColdRoomEntity>
    {
        public void Configure(EntityTypeBuilder<ColdRoomEntity> entityBuilder)
        {
            entityBuilder.ToTable("ColdRooms");

            entityBuilder.HasKey(e => e.ColdRoomId).HasName("ColdRooms_PK");

            entityBuilder.Property(e => e.ColdRoomId).HasColumnType("int");

            entityBuilder.Property(e => e.Number).HasColumnType("int");

            entityBuilder.Property(e => e.Description)
                            .HasColumnType("varchar(100)");

            entityBuilder.Property(e => e.Surface).HasColumnType("float");

            entityBuilder.Property(e => e.Capacity).HasColumnType("float");

            entityBuilder.Property(e => e.Temperature).HasColumnType("float");
            entityBuilder.Property(e => e.Humidity).HasColumnType("float");


        }
    }
}