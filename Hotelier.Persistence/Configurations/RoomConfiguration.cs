using Hotelier.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hotelier.Persistence.Configurations;

public class RoomConfiguration : IEntityTypeConfiguration<Room>
{
    public void Configure(EntityTypeBuilder<Room> builder)
    {
        builder.Property(c => c.CoverImage).IsRequired();
        builder.Property(c => c.Title).IsRequired();
        builder.Property(c => c.Description).IsRequired();
        builder.Property(c => c.RoomNumber).IsRequired();
        builder.Property(c => c.Price).IsRequired();
        builder.Property(c => c.BathCount).IsRequired();
        builder.Property(c => c.BedCount).IsRequired();
        builder.Property(c => c.WiFi).IsRequired();
    }
}