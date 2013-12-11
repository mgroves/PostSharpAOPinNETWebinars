using System.Reflection.Emit;
using System.Web;
using NUnit.Framework;
using PostSharp.Aspects;
using PostSharp.Aspects.Internals;
using Subtext.TestLibrary;

namespace UTExample.Tests
{
    [TestFixture]
    public class CacheAspectTest
    {
        [Test]
        public void if_a_value_is_cached_it_should_return_that_cached_value_on_entry()
        {
            var args = new MethodExecutionArgs(null, new ArgumentsArray(new object[] {"arg1"}))
            {
                Method = new DynamicMethod("Method1", null, null)
            };

            using (new HttpSimulator().SimulateRequest())
            {
                HttpContext.Current.Cache["Method1_arg1"] = "cached value";

                new CacheAspect().OnEntry(args);

                HttpContext.Current.Cache.Remove("Method1_arg1");
            }

            Assert.That(args.ReturnValue, Is.EqualTo("cached value"));
            Assert.That(args.FlowBehavior, Is.EqualTo(FlowBehavior.Return));
        }

        [Test]
        public void if_a_value_is_not_cached_it_should_proceed_on_entry()
        {
            var aspect = new CacheAspect();
            var args = new MethodExecutionArgs(null, new ArgumentsArray(new object[] {"arg1"}))
            {
                Method = new DynamicMethod("Method1", null, null)
            };

            using (new HttpSimulator().SimulateRequest())
            {
                aspect.OnEntry(args);
            }

            Assert.That(args.ReturnValue, Is.EqualTo(null));
            Assert.That(args.FlowBehavior, Is.EqualTo(FlowBehavior.Default));
        }
    }
}