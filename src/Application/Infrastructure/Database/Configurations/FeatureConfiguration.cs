using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Sirus.Application.Entities;

namespace Sirus.Application.Infrastructure.Database.Configurations;

public class FeatureConfiguration : IEntityTypeConfiguration<Feature>
{
    public void Configure(EntityTypeBuilder<Feature> builder)
    {
        builder.Ignore(e => e.DomainEvents);

        // add builder.Property configuration here
    }
}
