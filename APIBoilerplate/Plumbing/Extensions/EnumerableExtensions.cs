using System;
using System.Collections.Generic;

namespace APIBoilerplate.Plumbing.Extensions;

public static class EnumerableExtensions
{
    public static void ForEach<TSource>(
        this IEnumerable<TSource> source, 
        Action<TSource> func)
    {
        foreach (var item in source)
        {
            func(item);
        }
    }
}