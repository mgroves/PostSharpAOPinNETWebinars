namespace UTExample
{
    public interface ICacheConcern
    {
        void OnEntry(ICacheConcernArgs args);
        void OnSuccess(ICacheConcernArgs args);
    }

    public class CacheConcern : ICacheConcern
    {
        readonly ICacheService _cacheService;

        public CacheConcern(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        public void OnEntry(ICacheConcernArgs args)
        {
            var cacheKey = args.MethodName + "_" + args.Arguments[0];
            if (!_cacheService.Exists(cacheKey))
                return;
            args.ReturnValue = _cacheService.Get(cacheKey);
            args.ReturnImmediately = true;
        }

        public void OnSuccess(ICacheConcernArgs args)
        {
            var cacheKey = args.MethodName + "_" + args.Arguments[0];
            _cacheService.Set(cacheKey, args.ReturnValue);
        }
    }
}