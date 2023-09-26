using Broker.Models;

namespace Broker.Services.Interfaces
{
    public interface IMessageStorageService
    {
        void add(Message message);
        Message getNext();

        bool isEmpty();

    }
}
