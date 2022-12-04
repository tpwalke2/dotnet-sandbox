using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Net7APIBoilerplate.Plumbing.Extensions;

public static class TypeExtensions
{
    public static IEnumerable<Type> GetConstructorParameterTypes(this Type type)
    {
        return type
               .GetConstructors(BindingFlags.Public | BindingFlags.Instance)
               .SelectMany(ci => ci.GetParameters())
               .GroupBy(pi => pi.ParameterType)
               .Select(g => g.First())
               .Select(pi => pi.ParameterType);
    }

    public static TAttribute GetAttribute<TAttribute>(this ICustomAttributeProvider type)
    {
        var tAttr = type.GetCustomAttributes(typeof(TAttribute), false).FirstOrDefault();
        return (TAttribute)tAttr;
    }
}