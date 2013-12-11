using System;
using PostSharp.Aspects;

namespace BeforeAndAfter.ConsoleDemo
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
        [MyBoundaryAspect]
        public void DoSomething()
        {
            Console.WriteLine("Do something");
        }
    }

    [Serializable]
    public class MyBoundaryAspect : OnMethodBoundaryAspect
    {
        public override void OnEntry(MethodExecutionArgs args)
        {
            Console.WriteLine("Before {0}", args.Method.Name);
        }

        public override void OnExit(MethodExecutionArgs args)
        {
            Console.WriteLine("After {0}", args.Method.Name);
        }
    }

    // other boundaries: OnSuccess, OnException
}
