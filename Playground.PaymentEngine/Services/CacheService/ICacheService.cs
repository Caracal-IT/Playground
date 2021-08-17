using System;

namespace Playground.PaymentEngine.Services.CacheService {
    public interface ICacheService {
        T GetValue<T>(string key, Func<T> getValue) where T : class;
    }
}