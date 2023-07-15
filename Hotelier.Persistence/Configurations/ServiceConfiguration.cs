using Hotelier.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hotelier.Persistence.Configurations;

public class ServiceConfiguration : IEntityTypeConfiguration<Service>
{
    public void Configure(EntityTypeBuilder<Service> builder)
    {
        builder.Property(c => c.Icon).IsRequired();
        builder.Property(c => c.Title).IsRequired();
        builder.Property(c => c.Description).IsRequired();
    }
}