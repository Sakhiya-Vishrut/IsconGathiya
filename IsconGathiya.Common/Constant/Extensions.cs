using IsconGathiya.Common.Cache;
using System.Collections.Concurrent;

namespace IsconGathiya.Common.Caching
{
    public static class CacheExtensions
    {
        public enum UserCachingTime
        {
            VeryShort = 2,
            SemiShort = 5,
            Short = 10,
            Medium = 30,
            Long = 60,
            SemiLong = 90,
            VeryLong = 3600
        }
        private static ConcurrentDictionary<string, int> keyinProgress = new ConcurrentDictionary<string, int>();

        public static T Get<T>(this ICacheManager cacheManager, string key, Func<T> acquire)
        {
            return Get(cacheManager, key, UserCachingTime.VeryLong.GetHashCode(), acquire);
        }
        public static T Get<T>(this ICacheManager cacheManager, string key, int cacheTime, Func<T> acquire)
        {
            if (!ConfigItems.IsCacheActive)
                return acquire();


            if (ConfigItems.CacheInSleepWhileRequestIsInProgress)
            {
                #region Badal Code
                bool check = false;
            fromstart: if (cacheManager.IsSet(key))
                {
                    return cacheManager.Get<T>(key, ref check);
                }
                int intTemp;

                if (keyinProgress.TryGetValue(key, out intTemp))
                {
                    if (intTemp != Environment.CurrentManagedThreadId)
                    {
                        do
                        {
                            Thread.Sleep(10);
                        } while (keyinProgress.ContainsKey(key));
                        goto fromstart;
                    }
                    else
                    {
                        try
                        {
                            var result = acquire();
                            if (cacheTime > 0)
                            {
                                cacheManager.Set(key, result, cacheTime);
                            removeagain2: if (!keyinProgress.TryRemove(key, out intTemp))
                                {
                                    Thread.Sleep(25);
                                    goto removeagain2;
                                }
                            }
                            return result;
                        }
                        catch
                        {
                            keyinProgress.TryRemove(key, out intTemp);

#pragma warning disable CS8603 
                            return default(T);
#pragma warning restore CS8603
                        }
                    }
                }
                else
                {
                    try
                    {
                        //key not found - its new add it and execute the function
                        keyinProgress.TryAdd(key, Environment.CurrentManagedThreadId);
                        var result = acquire();
                        if (cacheTime > 0)
                        {
                            cacheManager.Set(key, result, cacheTime);
                        removeagain: if (!keyinProgress.TryRemove(key, out intTemp))
                            {
                                Thread.Sleep(25);
                                goto removeagain;
                            }
                        }
                        return result;
                    }
                    catch (Exception)
                    {
                        keyinProgress.TryRemove(key, out intTemp);
#pragma warning disable CS8603 
                        return default(T);
#pragma warning restore CS8603 
                    }
                }
                #endregion
            }
            else
            {
                #region Vishal Code
                bool check_1 = false;
                if (cacheManager.IsSet(key))
                {
                    return cacheManager.Get<T>(key, ref check_1);
                }
                var result_1 = acquire();
                if (cacheTime > 0)
                    cacheManager.Set(key, result_1, cacheTime);
                return result_1;
                #endregion
            }
        }

        public static bool IsSet(this ICacheManager cacheManager, string key)
        {
            if (!ConfigItems.IsCacheActive)
                return false;
            return cacheManager.IsSet(key);
        }

        public static void Set(this ICacheManager cacheManager, string key, object? result, int cacheTime)
        {
            if (cacheTime > 0)
                cacheManager.Set(key, result, cacheTime);
        }


    }
}
