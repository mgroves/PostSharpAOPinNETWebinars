using System.Web;
using Moq;
using NUnit.Framework;
using System.Reflection.Emit;
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
            var argsMock = new Mock<ICacheConcernArgs>();
            argsMock.Setup(a => a.MethodName).Returns("Method1");
            argsMock.Setup(a => a.Arguments).Returns(new object[] { "arg1" });
            argsMock.SetupProperty(a => a.ReturnValue);
            argsMock.SetupProperty(a => a.ReturnImmediately);

            using (new HttpSimulator().SimulateRequest())
            {
                HttpContext.Current.Cache["Method1_arg1"] = "cached value";

                new CacheConcern().OnEntry(argsMock.Object);

                HttpContext.Current.Cache.Remove("Method1_arg1");
            }

            Assert.That(argsMock.Object.ReturnValue, Is.EqualTo("cached value"));
            Assert.That(argsMock.Object.ReturnImmediately, Is.EqualTo(true));
        }

        [Test]
        public void if_a_value_is_not_cached_it_should_proceed_on_entry()
        {
            var argsMock = new Mock<ICacheConcernArgs>();
            argsMock.Setup(a => a.MethodName).Returns("Method1");
            argsMock.Setup(a => a.Arguments).Returns(new object[] { "arg1" });
            argsMock.SetupProperty(a => a.ReturnValue);
            argsMock.SetupProperty(a => a.ReturnImmediately);

            using (new HttpSimulator().SimulateRequest())
            {
                new CacheConcern().OnEntry(argsMock.Object);
            }

            Assert.That(argsMock.Object.ReturnValue, Is.EqualTo(null));
            Assert.That(argsMock.Object.ReturnImmediately, Is.EqualTo(false));
        }
    }
}