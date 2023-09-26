using Common;
using Grpc.Net.Client;
using GrpcAgent;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting.Server.Features;

namespace Broker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = WebHost.CreateDefaultBuilder(args)
                 .UseStartup<Startup>()
                 .UseUrls(EndpointConstants.subscriberAddress)
                 .Build();

            host.Start(); // start nu blocheaza firul

            subscribe(host);

            Console.WriteLine("Press enter to exit");
            Console.ReadLine();
        }

        private static void subscribe(IWebHost host)
        {
            var channel = GrpcChannel.ForAddress(EndpointConstants.brokerAddress);
            var client = new Subscriber.SubscriberClient(channel);

            var address = host.ServerFeatures.Get<IServerAddressesFeature>().Addresses.First();
            Console.WriteLine($"Subscriber listenin at : {address}");

            Console.Write("Enter the topic: ");
            var topic = Console.ReadLine().ToLower();

            var request = new SubscriberRequest() { Topic = topic, Address = address};
            try
            {
                var reply = client.subscribe(request);
                Console.WriteLine($"Subscribed reply : {reply.IsSuccess}");
            } catch (Exception ex) { Console.WriteLine($"Error subscriber : {ex.Message}"); }
        }
    }
}