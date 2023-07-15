using Hotelier.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hotelier.Persistence.Configurations;

public class SubscribeConfiguration : IEntityTypeConfiguration<Subscribe>
{
    public void Configure(EntityTypeBuilder<Subscribe> builder)
    {
        builder.Property(c => c.Mail).IsRequired();
    }
}