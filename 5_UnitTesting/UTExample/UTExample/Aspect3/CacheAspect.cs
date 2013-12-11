using System;
using System.Reflection;
using PostSharp.Aspects;

namespace UTExample
{
    [Serializable]
    public class CacheAspect : OnMethodBoundaryAspect
    {
        public static bool On = true;
        ICacheConcern _concern;

        public override void RuntimeInitialize(MethodBase method)
        {
            if (!On) return;
            _concern = new CacheConcern();  // use IoC container here
        }

        public override void OnEntry(MethodExecutionArgs args)
        {
            if (!On) return;
            _concern.OnEntry(new MethodExecutionArgsAdapter(args));
        }

        public override void OnSuccess(MethodExecutionArgs args)
        {
            if (!On) return;
            _concern.OnSuccess(new MethodExecutionArgsAdapter(args));
        }
    }
}