using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Linq.Expressions;
using Tizzani.ValueObject.Extensions;

namespace Tizzani.ValueObject.EntityFrameworkCore;
public static class EntityTypeBuilderExtensions
{
    public static PropertyBuilder HasValueObject(this EntityTypeBuilder builder, Type valueObjectType, string navigationName)
    {
        if (!valueObjectType.IsValueObject())
            throw new InvalidOperationException();

        return builder.OwnsOne(valueObjectType, navigationName).Property("Value");
    }

    public static PropertyBuilder HasValueObject<TEntity, TValueObject>(this EntityTypeBuilder<TEntity> builder, Expression<Func<TEntity, TValueObject?>> navigationExpression)
        where TEntity : class
        where TValueObject : class
    {
        if (!typeof(TValueObject).IsValueObject())
            throw new InvalidOperationException();

        return builder.OwnsOne(navigationExpression).Property("Value");
    }
}