namespace Broker.Models
{
    public class Message
    {
        public Message(string topic, string content)
        {
            this.content = content;
            this.topic = topic;
        }

        public string topic { get; }
        public string content { get; }
    }
}
