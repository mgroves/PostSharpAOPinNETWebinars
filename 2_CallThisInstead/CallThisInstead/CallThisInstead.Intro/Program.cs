using System;
using System.Linq;
using PostSharp.Aspects;

namespace CallThisInstead.Intro
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
        [MyInterceptionAspect]
        public void DoSomething()
        {
            Console.WriteLine("Do something");
        }
    }

    [Serializable]
    public class MyInterceptionAspect : MethodInterceptionAspect
    {
        public override void OnInvoke(MethodInterceptionArgs args)
        {
            Console.WriteLine("Before calling {0}", args.Method.Name);
            args.Proceed();
            Console.WriteLine("After calling {0}", args.Method.Name);
        }
    }
}
