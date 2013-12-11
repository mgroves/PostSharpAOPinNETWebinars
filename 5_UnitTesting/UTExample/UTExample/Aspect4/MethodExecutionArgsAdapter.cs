using PostSharp.Aspects;

namespace UTExample
{
    public interface ICacheConcernArgs
    {
        string MethodName { get; }
        object[] Arguments { get; }
        object ReturnValue { get; set; }
        bool ReturnImmediately { get; set; }
    }

    public class MethodExecutionArgsAdapter : ICacheConcernArgs
    {
        readonly MethodExecutionArgs _args;

        public MethodExecutionArgsAdapter(MethodExecutionArgs args)
        {
            _args = args;
        }

        public string MethodName
        {
            get { return _args.Method.Name; }
        }
        public object[] Arguments
        {
            get { return _args.Arguments.ToArray(); }
        }
        public object ReturnValue
        {
            get { return _args.ReturnValue; }
            set { _args.ReturnValue = value; }
        }
        public bool ReturnImmediately
        {
            get { return _args.FlowBehavior == FlowBehavior.Return; }
            set { if (value) _args.FlowBehavior = FlowBehavior.Return; }
        }
    }
}