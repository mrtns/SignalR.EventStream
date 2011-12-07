using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using SignalR.Hubs;

namespace SignalR
{
    public class EventStream : Hub, IEventStream
    {

        public void SendNotice(string @event)
        {
            SendNotice("event", @event);
        }

        public void SendNotice<T>(T @event) where T : ISignalREvent
        {
            SendNotice(@event.Type, @event.Event);
        }

        public void SendNotice(string type, object @event)
        {
            GetClients<EventStream>()["authorized"]
                .receiveEvent(JsonConvert.SerializeObject(
                    new {
                        Type = type,
                        Event = @event
                    }));
        }

        public bool Authorize()
        {
            //validate user id
            string id = Context.ClientId == "null" ? null : Context.ClientId;

            if (id != null) {
                AddToGroup("authorized");
                return true;
            }

            return false;
        }
    }
}
