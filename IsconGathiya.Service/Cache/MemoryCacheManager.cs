
using IsconGathiya.Common;
using IsconGathiya.Common.Cache;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;

namespace IsconGathiya.Service.Cache
{
    public partial class MemoryCacheManager : ICacheManager
    {
        private IMemoryCache Cache;
        private readonly ILogger<MemoryCacheManager> _logger;
        private readonly object _lock = new object();
        private static ConcurrentDictionary<string, int> allKeys = new ConcurrentDictionary<string, int>();

        public MemoryCacheManager(IMemoryCache _cache, ILogger<MemoryCacheManager> logger)
        {
            Cache = _cache;
            _logger = logger;
            _logger.LogInformation("Memory Cache Manager initialized.");
        }

        public virtual T Get<T>(string key, ref bool hasValue)
        {
            return (T)Cache.Get(key);
        }

        public virtual void Set(string key, object? data, int cacheTime)
        {
            if (data == null)
                return;

            var timeSpan = TimeSpan.FromMinutes(cacheTime);
            var cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(timeSpan);
            Cache.Set(key, data, cacheEntryOptions);

            SetKeys(key);
        }

        public virtual bool IsSet(string key)
        {
            return Cache.TryGetValue(key, out _);
        }

        private void SetKeys(string key)
        {
            try
            {
                if (ConfigItems.IsLockCustomCacheMethods)
                {
                    lock (_lock)
                    {
                        SetKeysInternal(key);
                    }
                }
                else
                {
                    SetKeysInternal(key);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "From Service > Exception in custom set keys : {@key}", key);
            }
        }

        private void SetKeysInternal(string key)
        {
            try
            {
                allKeys.TryAdd(key, 0);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "From Service > Exception in custom set keys add lst method key : {@key}, Count : {@count}", key, allKeys.Count);
            }
        }

        public virtual void RemoveByPattern(string pattern)
        {
            try
            {
                ConcurrentDictionary<string, int> lst = (ConfigItems.CacheWithRemoveOtherSite ? allKeys : new ConcurrentDictionary<string, int>());
                List<string> keysToRemove = new List<string>();
                if (lst != null && !lst.IsEmpty)
                {
                    try
                    {
                        var filter = lst.Where(d => d.Key.Contains(pattern));
                        if (filter != null && filter.Any())
                            keysToRemove = lst.Where(d => d.Key.Contains(pattern)).Select(d => d.Key).ToList();
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "From Service > Exception in RemoveByPattern pattern : {@pattern}", pattern);
                    }

                    if (keysToRemove != null && keysToRemove.Count > 0)
                    {
                        foreach (string key in keysToRemove)
                            Remove(key);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "From Service > Exception in RemoveByPattern pattern : {@pattern}", pattern);
            }
        }

        public virtual void Remove(string key)
        {
            try
            {
                Cache.Remove(key);
                RemoveKeys(key);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "From Service > Exception in Remove key : {@key}", key);
            }
        }

        private void RemoveKeys(string key)
        {
            try
            {
                if (ConfigItems.IsLockCustomCacheMethods)
                {
                    lock (_lock)
                    {
                        RemoveKeysInternal(key);
                    }
                }
                else
                {
                    RemoveKeysInternal(key);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "From Service > Exception in custom remove key : {@key}", key);
            }
        }

        private void RemoveKeysInternal(string key)
        {
            if (allKeys != null)
            {
                try
                {
                    int intTemp;
                    allKeys.TryRemove(key, out intTemp);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "From Service > Exception in custom remove key while removing from list : {@key}", key);
                }
            }
        }
    }
}