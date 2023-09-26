using Broker.Models;

namespace Broker.Services.Interfaces
{
    public interface IConnectionStorageService
    {
        void add(Connection connection);
        void remove(string address);

        IList<Connection> getConnectionsByTopic(string topic);
    }
}
