using Hotelier.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hotelier.Persistence.Configurations;

public class StaffConfiguration : IEntityTypeConfiguration<Staff>
{
    public void Configure(EntityTypeBuilder<Staff> builder)
    {
        builder.Property(c => c.Title).IsRequired();
        builder.Property(c => c.Name).IsRequired();
        builder.Property(c => c.Image).IsRequired();
        builder.Property(c => c.SocialMediaFacebook).IsRequired();
        builder.Property(c => c.SocialMediaInstagram).IsRequired();
        builder.Property(c => c.SocialMediaTwitter).IsRequired();
    }
}