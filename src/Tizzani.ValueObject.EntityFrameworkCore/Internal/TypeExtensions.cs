﻿namespace Tizzani.ValueObject.EntityFrameworkCore.Internal;
internal static class TypeExtensions
{
    public static bool IsValueObject(this Type t)
    {
        if (t.BaseType is not Type baseType || !baseType.IsGenericType)
            return false;

        var genericType = baseType.GetGenericTypeDefinition();
        return typeof(ValueObject<>).IsAssignableFrom(genericType);
    }
}