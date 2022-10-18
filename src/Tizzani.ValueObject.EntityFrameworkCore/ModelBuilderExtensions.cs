using Microsoft.EntityFrameworkCore;
using Tizzani.ValueObject.EntityFrameworkCore.Internal;

namespace Tizzani.ValueObject.EntityFrameworkCore;

public static class ModelBuilderExtensions
{
    public static ModelBuilder MapValueObjects(this ModelBuilder builder)
    {
        foreach (var entityType in builder.Model.GetEntityTypes().Select(e => e.ClrType))
            foreach (var property in entityType.GetProperties().Where(p => p.PropertyType.IsValueObject()))
                builder.Entity(entityType).HasValueObject(property.PropertyType, property.Name).HasColumnName(property.Name);

        return builder;
    }
}