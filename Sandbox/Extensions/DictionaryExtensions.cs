using System;
using System.Collections.Generic;

namespace Sandbox.Extensions;

public static class DictionaryExtensions
{
    public static TValue? TryGetOrDefault<TKey, TValue>(
        this IDictionary<TKey, TValue>? dictionary,
        TKey key,
        TValue? defaultValue = default)
    {
        if (dictionary == null || key == null) return defaultValue;

        return dictionary.TryGetValue(key, out var result) ? result : defaultValue;
    }
        
    public static TValue TryGetOrDefault<TKey, TValue>(
        this IDictionary<TKey, TValue>? dictionary,
        TKey key,
        Func<TValue> createDefaultValue)
    {
        if (dictionary == null || key == null) return createDefaultValue.Invoke();
            
        return dictionary.TryGetValue(key, out var result) ? result : createDefaultValue.Invoke();
    }
}