using System.Web;

namespace UTExample
{
    public interface ICacheConcern
    {
        void OnEntry(ICacheConcernArgs args);
        void OnSuccess(ICacheConcernArgs args);
    }

    public class CacheConcern : ICacheConcern
    {
        public void OnEntry(ICacheConcernArgs args)
        {
            var cacheKey = args.MethodName + "_" + args.Arguments[0];
            if (HttpContext.Current.Cache[cacheKey] == null)
                return;
            args.ReturnValue = HttpContext.Current.Cache[cacheKey];
            args.ReturnImmediately = true;
        }

        public void OnSuccess(ICacheConcernArgs args)
        {
            var cacheKey = args.MethodName + "_" + args.Arguments[0];
            HttpContext.Current.Cache[cacheKey] = args.ReturnValue;
        }
    }
}