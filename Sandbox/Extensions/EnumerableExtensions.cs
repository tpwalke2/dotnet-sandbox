using System;
using System.Collections.Generic;
using System.Linq;

namespace Sandbox.Extensions;

public static class EnumerableExtensions
{
    public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> func)
    {
        foreach (var item in enumerable)
        {
            func(item);
        }
    }

    public static IEnumerable<(TItem1, TItem2)> Combinate<TItem1, TItem2>(
        this IEnumerable<TItem1> source,
        IEnumerable<TItem2> other)
    {
        if (other == null || source == null) return Enumerable.Empty<(TItem1, TItem2)>();

        IEnumerable<(TItem1, TItem2)> seed = new (TItem1, TItem2)[] {};

        return source.Aggregate(seed,
                                (current, next) => current.Concat(other.Select(x => (next, x))));
    }
}