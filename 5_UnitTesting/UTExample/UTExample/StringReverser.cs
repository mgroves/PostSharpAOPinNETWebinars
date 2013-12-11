using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using PostSharp.Aspects;

namespace UTExample
{
    public class StringReverser : IStringReverser
    {
        [CacheAspect]
        public string Reverse(string text)
        {
            if (text == null)
                return null;
            return new string(text.Reverse().ToArray());
        }
    }

    [Serializable]
    public class LoggingAspect : MethodInterceptionAspect
    {
        IMyLogger _log;

        public override void RuntimeInitialize(MethodBase method)
        {
            _log = MyServiceLocator.Get<IMyLogger>();
        }

        public override void OnInvoke(MethodInterceptionArgs args)
        {
            var methodName = args.Method.Name;
            var parameters = args.Method.GetParameters();
            
            _log.WriteLine(string.Format("{0} timestamp: {1}", methodName, DateTime.Now));
            
            for (var i = 0; i < args.Arguments.Count; i++)
                _log.WriteLine(string.Format("{0} argument #{1}, {2} ({3}): {4}",
                    methodName,
                    i,
                    parameters[i].Name,
                    parameters[i].ParameterType,
                    args.Arguments[i]));

            args.Proceed();
            
            _log.WriteLine(string.Format("{0} return value: {1}", methodName, args.ReturnValue));
        }
    }

    public class MyServiceLocator
    {
        public static T Get<T>()
        {
            throw new NotImplementedException();
        }
    }

//    public class StringReverserWithLogging : StringReverser
//    {
//        IMyLogger _log;
//
//        public StringReverserWithLogging(IMyLogger logger)
//        {
//            _log = logger;
//        }
//
//        public new string Reverse(string text)
//        {
//            _log.WriteLine("MyFunction: " + DateTime.Now);
//            _log.WriteLine("text (string) argument: " + text);
//
//            var result = base.Reverse(text);
//
//            _log.WriteLine("Return value: " + result);
//
//            return result;
//        }
//    }

    public class StringReverserWithLogging : IStringReverser
    {
	    IMyLogger _log;
        StringReverser _reverser;

        public StringReverserWithLogging(StringReverser reverser, IMyLogger logger)
        {
            _reverser = reverser;
		    _log = logger;
	    }

        public string Reverse(string text)
        {
            _log.WriteLine("MyFunction: " + DateTime.Now);
            _log.WriteLine("text (string) argument: " + text);

            var result = _reverser.Reverse(text);

            _log.WriteLine("Return value: " + result);

            return result;
        }
    }

    public interface IStringReverser
    {
        string Reverse(string text);
    }

    public interface IMyLogger
    {
        void WriteLine(string test);
    }

    public class MyLogger : IMyLogger
    {
        public void WriteLine(string test)
        {
            Debug.WriteLine(test);
        }
    }
}