using Grpc.Core;
using GrpcAgent;

namespace Receiver
{
    public class NotificationService : Notifier.NotifierBase
    {
        public override Task<NotifyReply> notify(NotifyRequest request, ServerCallContext context)
        {
            Console.WriteLine($"Received : {request.Content}");
            return Task.FromResult( new NotifyReply() { IsSuccess = true} );
        }
    }
}
