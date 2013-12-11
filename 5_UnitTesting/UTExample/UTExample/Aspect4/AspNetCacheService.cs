using System.Web;

namespace UTExample
{
    public interface ICacheService
    {
        bool Exists(string cacheKey);
        object Get(string cacheKey);
        void Set(string cacheKey, object value);
    }

    public class AspNetCacheService : ICacheService
    {
        public bool Exists(string cacheKey)
        {
            return HttpContext.Current.Cache[cacheKey] != null;
        }

        public object Get(string cacheKey)
        {
            return HttpContext.Current.Cache[cacheKey];
        }

        public void Set(string cacheKey, object value)
        {
            HttpContext.Current.Cache[cacheKey] = value;
        }
    }
}