using System;
using System.Reflection;
using PostSharp.Aspects;

namespace UnderTheHood
{
    class Program
    {
        static void Main(string[] args)
        {
            var obj = new MyClass();
            obj.DoSomething();
        }
    }

    public class MyClass
    {
        [MyAspect]
        public void DoSomething()
        {
            Console.WriteLine("Do Something!");
        }
    }

//    [Serializable]
//    public class MyAspect : OnMethodBoundaryAspect
//    {
//        public override void OnEntry(MethodExecutionArgs args)
//        {
//            Console.WriteLine("OnEntry of {0}", args.Method.Name);
//        }

//        public override void OnExit(MethodExecutionArgs args)
//        {
//            Console.WriteLine("OnExit of {0}", args.Method.Name);
//        }
//
//        public override void OnSuccess(MethodExecutionArgs args)
//        {
//            Console.WriteLine("OnSuccess of {0}", args.Method.Name);
//        }
//
//        public override void OnException(MethodExecutionArgs args)
//        {
//            Console.WriteLine("OnException of {0}", args.Method.Name);
//        }
//    } 
    
    [Serializable]
    public class MyAspect : OnMethodBoundaryAspect
    {
        string _methodName;

        public override void CompileTimeInitialize(MethodBase method, AspectInfo aspectInfo)
        {
            _methodName = method.Name;
        }

        public override void OnEntry(MethodExecutionArgs args)
        {
            Console.WriteLine("OnEntry of {0}", _methodName);
        }

//        public override void OnExit(MethodExecutionArgs args)
//        {
//            Console.WriteLine("OnExit of {0}", _methodName);
//        }
//
//        public override void OnSuccess(MethodExecutionArgs args)
//        {
//            Console.WriteLine("OnSuccess of {0}", _methodName);
//        }
//
//        public override void OnException(MethodExecutionArgs args)
//        {
//            Console.WriteLine("OnException of {0}", _methodName);
//        }
    }
}
