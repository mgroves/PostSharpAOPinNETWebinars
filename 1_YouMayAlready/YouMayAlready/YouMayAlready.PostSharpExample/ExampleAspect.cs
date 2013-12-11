using System;
using PostSharp.Aspects;

namespace YouMayAlready.PostSharpExample
{
    [Serializable]
    public class ExampleAspect : OnMethodBoundaryAspect
    {
        public override void OnEntry(MethodExecutionArgs args)
        {
            Console.WriteLine("Before method: {0}", args.Method.Name);
        }

        public override void OnSuccess(MethodExecutionArgs args)
        {
            Console.WriteLine("After method: {0}", args.Method.Name);
        }
    }
}