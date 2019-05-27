using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using HexagonalExample.Domain.Contracts.Adapters;

namespace HexagonalExample.Infrastructure.Caching.Memory
{
    public class CacheAdapter : ICacheAdapter
    {
        #region Constants

        private const double CacheExpirationInMinutes = 10;

        #endregion Constants

        #region Fields

        private readonly MemoryCache _memoryCache;

        #endregion Fields

        #region Constructors

        public CacheAdapter()
        {
            _memoryCache = MemoryCache.Default;
        }

        #endregion Constructors

        #region Methods

        public void Set<TValue>(string key, TValue value)
            where TValue : class
        {
            _memoryCache.Add(key, value, DateTime.Now.AddMinutes(CacheExpirationInMinutes));
        }

        public TValue Get<TValue>(string key)
            where TValue : class
        {
            return _memoryCache.Get(key) as TValue;
        }

        public void Delete(string key)
        {
            if (_memoryCache.Contains(key))
            {
                _memoryCache.Remove(key);
            }
        }

        public void Clear()
        {
            List<string> cacheKeys = _memoryCache.Select(x => x.Key).ToList();

            foreach (var cacheKey in cacheKeys)
            {
                Delete(cacheKey);
            }
        }

        #endregion Methods
    }
}
