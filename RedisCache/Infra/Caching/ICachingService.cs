namespace RedisCache.Infra.Caching
{
    public interface ICachingService
    {
        Task<string?> GetAsync(string key);
        Task SetAsync(string key, string value);
    }
}