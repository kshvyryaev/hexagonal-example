namespace HexagonalExample.Domain.Contracts.Adapters
{
    public interface ICacheAdapter
    {
        void Set<TValue>(string key, TValue value)
            where TValue : class;

        TValue Get<TValue>(string key)
            where TValue : class;

        void Update<TValue>(string key, TValue value)
            where TValue : class;

        void Delete(string key);

        void Clear();
    }
}
