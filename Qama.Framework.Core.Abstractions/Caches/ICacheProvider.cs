using System;
using System.Threading.Tasks;
using Qama.Framework.Core.Abstractions.Caches.Options;

namespace Qama.Framework.Core.Abstractions.Caches
{
    public interface ICacheProvider
    {
        void Set<T>(string key, T value, CacheDomain domain, LifeSpanCacheOption option = LifeSpanCacheOption.Forever, TimeSpan? timeSpan = null) where T : class;
        Task SetAsync<T>(string key, T value, CacheDomain domain, LifeSpanCacheOption option = LifeSpanCacheOption.Forever, TimeSpan? timeSpan = null) where T : class;

        T Get<T>(string key, CacheDomain domain) where T : class;
        Task<T> GetAsync<T>(string key, CacheDomain domain) where T : class;

        void Remove(string key, CacheDomain domain);
        Task RemoveAsync(string key, CacheDomain domain);

        void Refresh(string key, CacheDomain domain);
        Task RefreshAsync(string key, CacheDomain domain);
    }
}
