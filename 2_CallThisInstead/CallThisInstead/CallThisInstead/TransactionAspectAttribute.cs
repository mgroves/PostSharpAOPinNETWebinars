using System;
using System.Reflection;
using System.Transactions;
using PostSharp.Aspects;

namespace CallThisInstead
{
    [Serializable]
    public class TransactionAspectAttribute : MethodInterceptionAspect
    {
        string _methodName;
        string _methodDeclaringTypeName;

        public override void CompileTimeInitialize(MethodBase method, AspectInfo aspectInfo)
        {
            _methodDeclaringTypeName = method.DeclaringType.Name;
            _methodName = method.Name;
        }

        public override void OnInvoke(MethodInterceptionArgs args)
        {
            using (var scope = new TransactionScope())
            {
                try
                {
                    args.Proceed();
                    scope.Complete();
                }
                catch
                {
                    //Console.WriteLine("Unable to complete transaction in {0}::{1}",
                    //    args.Method.DeclaringType.Name,
                    //    args.Method.Name);
                    Console.WriteLine("Unable to complete transaction in {0}::{1}",
                        _methodDeclaringTypeName,
                        _methodName);
                    Console.WriteLine();
                }
            }
        }
    }
}