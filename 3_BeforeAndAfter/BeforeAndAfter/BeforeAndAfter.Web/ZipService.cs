using System;
using System.Threading;

namespace BeforeAndAfter.Web
{
    public class ZipService
    {
        [CacheAspect]
        public ZipInformation GetInformationFor(string zipCode)
        {
            Thread.Sleep(7000); // simulate slow web service / db call
            var rand = new Random();
            return new ZipInformation
                       {
                           MedianIncome = (rand.Next(350000,750000) / 10.0M),
                           Population = rand.Next(500,300000),
                           SquareMiles = rand.Next(100,500)
                       };
        }
    }
}