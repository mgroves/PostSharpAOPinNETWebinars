using NUnit.Framework;

namespace UTExample.Tests
{
    [TestFixture]
    public class ReverseTests
    {
        [SetUp]
        public void Setup()
        {
            CacheAspect.On = false;
        }

        [Test]
        public void it_should_reverse_hello_to_olleh()
        {
            var reverser = new StringReverser();
            var result = reverser.Reverse("hello");
            Assert.That(result, Is.EqualTo("olleh"));
        }

        [Test]
        public void it_should_return_null_if_given_null()
        {
            var reverser = new StringReverser();
            var result = reverser.Reverse(null);
            Assert.That(result, Is.EqualTo(null));
        }

        [Test]
        public void it_should_return_empty_string_if_given_empty_string()
        {
            var reverser = new StringReverser();
            var result = reverser.Reverse("");
            Assert.That(result, Is.EqualTo(""));
        }

        [Test]
        public void it_should_reverse_white_space_characters_too()
        {
            var reverser = new StringReverser();
            var result = reverser.Reverse("\r\n\t ");
            Assert.That(result, Is.EqualTo(" \t\n\r"));
        }
    }
}