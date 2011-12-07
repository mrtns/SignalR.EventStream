namespace SignalR
{
    public interface ISignalREvent
    {
        string Type { get; set; }
        object Event { get; set; }
    }
}
