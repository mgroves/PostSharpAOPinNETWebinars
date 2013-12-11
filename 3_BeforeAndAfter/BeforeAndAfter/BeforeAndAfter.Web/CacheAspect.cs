using System;
using System.Web;
using PostSharp.Aspects;

namespace BeforeAndAfter.Web
{
    [Serializable]
    public class CacheAspect : OnMethodBoundaryAspect
    {
        public override void OnEntry(MethodExecutionArgs args)
        {
            var cacheKey = args.Method.Name + "_" + args.Arguments[0];
            if (HttpContext.Current.Cache[cacheKey] == null)
                return;
            args.ReturnValue = HttpContext.Current.Cache[cacheKey];
            args.FlowBehavior = FlowBehavior.Return;
        }

        public override void OnSuccess(MethodExecutionArgs args)
        {
            var cacheKey = args.Method.Name + "_" + args.Arguments[0];
            HttpContext.Current.Cache[cacheKey] = args.ReturnValue;
        }
    }
}