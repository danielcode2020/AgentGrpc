

using Common;
using Grpc.Net.Client;
using GrpcAgent;

namespace Sender
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Publisher");
            var channel = GrpcChannel.ForAddress(EndpointConstants.brokerAddress);
            var client = new Publisher.PublisherClient(channel);
            while (true)
            {
                Console.Write("Enter the topic : ");
                var topic = Console.ReadLine().ToLower();

                Console.Write("Enter content : ");
                var content = Console.ReadLine();

                var request = new PublishRequest()
                {
                    Topic = topic,
                    Content = content
                };

                try
                {
                    var reply = await client.publishMessageAsync(request);
                    Console.WriteLine($"Publish reply : {reply.IsSuccess}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error publishing the message :{ex.Message}");
                }
               
            }
        }
    }
}
