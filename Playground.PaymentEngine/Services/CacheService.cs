using System;
using Microsoft.Extensions.Caching.Memory;
using Playground.Core;
using static System.TimeSpan;

namespace Playground.PaymentEngine.Services {
    public class CacheService: ICacheService {
        private readonly IMemoryCache _memoryCache;
        
        public CacheService(IMemoryCache memoryCache) => _memoryCache = memoryCache;
        
        public T GetValue<T>(string key, Func<T> getValue) where T : class {
            if (_memoryCache.TryGetValue(key, out T response)) return _memoryCache.Get(key) as T;
            
            response = getValue();
            _memoryCache.Set(key, response, new MemoryCacheEntryOptions().SetAbsoluteExpiration(FromMinutes(5)));
            return response;
        }
    }
}