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

            entityBuilder.HasKey(e => e.ColdRoomId)
                    .HasName("PRIMARY");

            entityBuilder.Property(e => e.ColdRoomId).HasColumnType("int(11)");

            entityBuilder.Property(e => e.Number).HasColumnType("int(11)");

            entityBuilder.Property(e => e.Description)
                            .HasColumnType("varchar(50)")
                            .HasCharSet("utf8mb4")
                            .HasCollation("utf8mb4_general_ci");

            entityBuilder.Property(e => e.Surface).HasColumnType("double");

            entityBuilder.Property(e => e.Capacity).HasColumnType("double");

            entityBuilder.Property(e => e.Temperature).HasColumnType("double");
            entityBuilder.Property(e => e.Humidity).HasColumnType("double");


        }
    }
}