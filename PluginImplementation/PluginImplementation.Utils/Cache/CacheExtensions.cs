using System;
using System.Collections.Generic;
using System.Linq;

namespace PluginImplementation.Utils.Cache
{
    public static class EFCacheExtensions
    {
        public static void SetCacheProvider(CacheProvider provider)
        {
            cacheProvider = provider;
        }

        private static CacheProvider cacheProvider;
        public static IEnumerable<T> AsCacheable<T>(this IQueryable<T> query)
        {
            if (cacheProvider == null)
            {
                throw new InvalidOperationException("Please set cache provider (call SetCacheProvider) before using caching");
            }
            return cacheProvider.GetOrCreateCache<T>(query);
        }

        public static IEnumerable<T> AsCacheable<T>(this IQueryable<T> query, TimeSpan cacheDuration)
        {
            if (cacheProvider == null)
            {
                throw new InvalidOperationException("Please set cache provider (call SetCacheProvider) before using caching");
            }
            return cacheProvider.GetOrCreateCache<T>(query, cacheDuration);
        }

        public static bool RemoveFromCache<T>(IQueryable<T> query)
        {
            if (cacheProvider == null)
            {
                throw new InvalidOperationException("Please set cache provider (call SetCacheProvider) before using caching");
            }
            return cacheProvider.RemoveFromCache<T>(query);
        }

        public static List<IPlugin> ToList<T>(this IQueryable<T> query, string folder)
        {
            return Utility.GetPlugins<IPlugin>(folder);
        }
    }
}
