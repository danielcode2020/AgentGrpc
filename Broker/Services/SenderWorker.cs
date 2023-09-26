using Broker.Services.Interfaces;
using Grpc.Core;
using GrpcAgent;

namespace Broker.Services
{
    public class SenderWorker : IHostedService
    {
        private Timer _timer;
        private const int TimeToWait = 2000;
        private readonly IMessageStorageService _messageStorage;
        private readonly IConnectionStorageService _connectionStorage;

        public SenderWorker(IServiceScopeFactory serviceScopeFactory)
        {
            using (var scope = serviceScopeFactory.CreateScope())
            {
                _messageStorage = scope.ServiceProvider.GetRequiredService<IMessageStorageService>();
                _connectionStorage = scope.ServiceProvider.GetRequiredService<IConnectionStorageService>();
            }
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(doSendWork, null, 0, TimeToWait);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        private void doSendWork(object state) 
        {
            while (!_messageStorage.isEmpty()) 
            {
                var message = _messageStorage.getNext();
                if (message != null)
                {
                    var connections = _connectionStorage.getConnectionsByTopic(message.topic);

                    foreach ( var connection in connections) 
                    {
                        var client = new Notifier.NotifierClient(connection.channel);
                        var request = new NotifyRequest() {Content = message.content};
                        try
                        {
                            var reply = client.notify(request);
                            Console.WriteLine($"Notified subscriber {connection.address} with {message.content}. Response {reply.IsSuccess}");
                        }
                        catch (RpcException rpcException)
                        {
                            if (rpcException.StatusCode == StatusCode.Internal)
                            {
                                _connectionStorage.remove(connection.address);
                            }
                            Console.WriteLine($"Rpc Error notifying subscriber {connection.address} . {rpcException.Message}")
                        }
                        catch (Exception ex )
                        {
                            Console.WriteLine($"Error notifying subscriber {connection.address}. {ex.Message}");
                        }
                    }
                }
            }
        }
    }
}
