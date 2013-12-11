using NUnit.Framework;

namespace UTExample.Tests
{
    [TestFixture]
    public class test
    {
        [Test]
        public void go()
        {
            var t = new StringReverserWithLogging(new MyLogger());
            var result = t.Reverse("hello");
            Assert.That(result, Is.EqualTo("olleh"));
        }
    }
}