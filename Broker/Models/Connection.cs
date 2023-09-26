using Grpc.Net.Client;

namespace Broker.Models
{
    public class Connection
    {

        public Connection(string address, string topic)
        {
            this.address = address;
            this.topic = topic;
            channel = GrpcChannel.ForAddress(address);
        }
        public string address {  get; }
        public string topic { get; }

        public GrpcChannel channel { get; }
    }
}
