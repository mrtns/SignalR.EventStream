using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Newtonsoft.Json;
using SignalR.Hubs;

namespace SignalR
{
    public class EventStream : Hub, IEventStream
    {

        public void Send(string @event)
        {
            Send("event", @event);
        }
        
        public void Send(string type, object @event)
        {
            GetClients<EventStream>()["authorized"]
                .receiveEvent(JsonConvert.SerializeObject(
                    new {
                        Type = type,
                        Event = @event
                    }));
        }

        public void Send(object @event)
        {
            if (Utilities.IsAnonymousType(@event.GetType())) {
                throw new InvalidOperationException(
                    "Anonymous types are not supported. Use Send(string, object) instead.");
            }

            string type = @event.GetType().Name;
            Send(type, @event);
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

        private static class Utilities
        {
            public static bool IsAnonymousType(Type type)
            {
                if (type == null)
                    throw new ArgumentNullException("type");

                return (type.IsClass
                        && type.IsSealed
                        && type.BaseType == typeof(object)
                        && type.Name.StartsWith("<>", StringComparison.Ordinal)
                        && type.IsDefined(typeof(CompilerGeneratedAttribute), true));
            }
        }

    }
}
