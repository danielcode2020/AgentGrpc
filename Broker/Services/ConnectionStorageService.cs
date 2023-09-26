using Broker.Models;
using Broker.Services.Interfaces;

namespace Broker.Services
{
    public class ConnectionStorageService : IConnectionStorageService
    {

        private readonly List<Connection> _connections;
        private readonly object _locker;

        public ConnectionStorageService()
        {
            _connections = new List<Connection>();
            _locker = new object();
        }
        public void add(Connection connection)
        {
            lock(_locker)
            {
                _connections.Add(connection);
            }
        }

        public IList<Connection> getConnectionsByTopic(string topic)
        {
            lock(_locker)
            {
                var filteredConnections = _connections.Where(x => x.topic == topic).ToList();
                return filteredConnections; 
            }
        }

        public void remove(string address)
        {
            lock (_locker)
            {
                _connections.RemoveAll(x => x.address == address);
            }
        }
    }
}
