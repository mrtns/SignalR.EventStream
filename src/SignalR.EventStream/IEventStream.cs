namespace SignalR
{
    public interface IEventStream
    {
        void SendNotice(string @event);
        void SendNotice<T>(T notice) where T : ISignalREvent;
        void SendNotice(string type, object notice);
    }
}