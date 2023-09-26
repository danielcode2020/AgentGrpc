using Broker.Models;
using Broker.Services.Interfaces;
using Grpc.Core;
using GrpcAgent;

namespace Broker.Services
{
    public class PublisherService : Publisher.PublisherBase
    {
        private readonly IMessageStorageService messageStorage;
        public PublisherService(IMessageStorageService messageStorageService)
        {
            messageStorage = messageStorageService;   
        }
        public override Task<PublishReply> publishMessage(PublishRequest request, ServerCallContext context)
        {

            Console.WriteLine($"Received : {request.Topic} {request.Content}");

            var message = new Message(request.Topic, request.Content);

            messageStorage.add(message);

            return Task.FromResult(new PublishReply()
            {
                IsSuccess = true
            });
        }
    }
}
