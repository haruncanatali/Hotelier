using Hotelier.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hotelier.Persistence.Configurations;

public class TestimonialConfiguration : IEntityTypeConfiguration<Testimonial>
{
    public void Configure(EntityTypeBuilder<Testimonial> builder)
    {
        builder.Property(c => c.Name).IsRequired();
        builder.Property(c => c.Image).IsRequired();
        builder.Property(c => c.Type).IsRequired();
        builder.Property(c => c.Description).IsRequired();
    }
}