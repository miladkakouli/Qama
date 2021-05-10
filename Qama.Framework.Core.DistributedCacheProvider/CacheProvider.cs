using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Qama.Framework.Core.Abstractions.Caches;
using Qama.Framework.Core.Abstractions.Caches.Options;
using Qama.Framework.Extensions.Serializer;

namespace Qama.Framework.Core.DistributedCacheProvider
{
    public class CacheProvider : ICacheProvider
    {
        private readonly IDistributedCache _distributedCache;
        private const string Delimeter = "_";
        public CacheProvider(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        private static string GenerateKey(string key, CacheDomain domain)
        {
            using MD5 md5 = MD5.Create();
            md5.Initialize();
            StringBuilder sb = new StringBuilder();
            foreach (byte b in md5.ComputeHash(Encoding.UTF8.GetBytes(domain.ToString())))
                sb.Append(b.ToString("X2"));
            return $"{key}{Delimeter}{sb}";
        }

        private static DistributedCacheEntryOptions GenerateCacheEntryOptions(LifeSpanCacheOption option, TimeSpan? timeSpan)
        {
            return option switch
            {
                LifeSpanCacheOption.Forever => new DistributedCacheEntryOptions(),
                LifeSpanCacheOption.Absolute => new DistributedCacheEntryOptions().SetAbsoluteExpiration(timeSpan.Value),
                LifeSpanCacheOption.RelativeFromLastAccess => new DistributedCacheEntryOptions().SetSlidingExpiration(timeSpan.Value),
                _ => new DistributedCacheEntryOptions()
            };
        }

        public void Set<T>(string key, T value, CacheDomain domain, LifeSpanCacheOption option = LifeSpanCacheOption.Forever, TimeSpan? timeSpan = null) where T : class
        {
            _distributedCache.Set(GenerateKey(key, domain), value.ToJsonByteArray(), GenerateCacheEntryOptions(option, timeSpan));
        }

        public Task SetAsync<T>(string key, T value, CacheDomain domain, LifeSpanCacheOption option = LifeSpanCacheOption.Forever, TimeSpan? timeSpan = null) where T : class
        {
            return _distributedCache.SetAsync(GenerateKey(key, domain), value.ToJsonByteArray(), GenerateCacheEntryOptions(option, timeSpan));
        }

        public T Get<T>(string key, CacheDomain domain) where T : class
        {
            return _distributedCache.Get(GenerateKey(key, domain)).FromJsonByteArray<T>();
        }

        public async Task<T> GetAsync<T>(string key, CacheDomain domain) where T : class
        {
            var result = await _distributedCache.GetAsync(GenerateKey(key, domain));
            return result.FromJsonByteArray<T>();
        }

        public void Remove(string key, CacheDomain domain)
        {
            _distributedCache.Remove(GenerateKey(key, domain));
        }

        public Task RemoveAsync(string key, CacheDomain domain)
        {
            return _distributedCache.RemoveAsync(GenerateKey(key, domain));
        }

        public void Refresh(string key, CacheDomain domain)
        {
            _distributedCache.Refresh(GenerateKey(key, domain));
        }

        public Task RefreshAsync(string key, CacheDomain domain)
        {
            return _distributedCache.RefreshAsync(GenerateKey(key, domain));
        }
    }
}
