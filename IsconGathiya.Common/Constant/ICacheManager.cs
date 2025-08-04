namespace IsconGathiya.Common.Cache
{
    public interface ICacheManager
    {
        T Get<T>(string key, ref bool hasValue);
        void Set(string key, object? data, int cacheTime);

        bool IsSet(string key);

        void RemoveByPattern(string pattern);

        void Remove(string key);
    }
}
