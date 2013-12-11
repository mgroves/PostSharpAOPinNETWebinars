using System;
using System.Linq;

namespace YouMayAlready.PostSharpExample
{
    public class BackwardsConsole
    {
        [ExampleAspect]
        public void Write(string text)
        {
            var backwardsText = new string(text.Reverse().ToArray());
            Console.WriteLine(backwardsText);
        }
    }
}