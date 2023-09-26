using Common;
using Microsoft.AspNetCore;

namespace Broker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebHost.CreateDefaultBuilder(args)
                 .UseStartup<Startup>()
                 .UseUrls(EndpointConstants.brokerAddress)
                 .Build()
                 .Run();

        }
    }
}