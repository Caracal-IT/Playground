namespace Playground.Core;

public interface ICacheService {
    T GetValue<T>(string key, Func<T> getValue) where T : class;
}