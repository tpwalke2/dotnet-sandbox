using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Sandbox.Extensions;

namespace Sandbox.Util
{
    public class Cache<TKey, TValue>
    {
        private readonly Func<TKey, TValue> _createNewItem;
        private readonly IDictionary<TKey, TValue> _items;

        /// <summary>
        /// Creates a cache backed by a non thread-safe collection. Use this for cases
        /// when multi-threading is not required as the overhead required for thread
        /// safety can reduce performance.
        /// </summary>
        /// <param name="createNewItem">Function that generates a new item on a cache miss</param>
        public Cache(Func<TKey, TValue> createNewItem) : this(createNewItem, false)
        {
        }

        /// <summary>
        /// Creates a cache backed by a collection that can be either thread-safe or
        /// not. If the cache will not be used in a multi-threaded case, do not use
        /// the thread-safe version.
        /// </summary>
        /// <param name="createNewItem">Function that generates a new item on a cache miss</param>
        /// <param name="threadSafe">setting to indicate thread-safe requirement</param>
        public Cache(Func<TKey, TValue> createNewItem, bool threadSafe)
        {
            _createNewItem = createNewItem;
            _items = threadSafe
                ? (IDictionary<TKey, TValue>) new ConcurrentDictionary<TKey, TValue>()
                : new Dictionary<TKey, TValue>();
        }

        /// <summary>
        /// Retrieves an item from the cache. If the cache misses, a new item will be
        /// generated and automatically added to the cache for future retrievals.
        ///
        /// If the item generator function returns the default for TValue, the cache
        /// will not be updated and a future retrieval for the same key will execute
        /// the item generator function again.  
        /// </summary>
        /// <param name="key">cache key</param>
        /// <returns></returns>
        public TValue Get(TKey key)
        {
            if (_items.TryGetValue(key, out var result)) return result;

            return _createNewItem(key).Pipe(newItem =>
            {
                if (!newItem.IsDefault()) _items.TryAdd(key, newItem);

                return newItem;
            });
        }
    }
}
