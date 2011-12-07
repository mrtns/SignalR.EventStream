namespace SignalR
{
    public interface IEventStream
    {
        void Send(string @event);
        void Send(string type, object notice);
        void Send(object @event);
    }
}