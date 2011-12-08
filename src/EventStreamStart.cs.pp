[assembly: WebActivator.PreApplicationStartMethod(typeof($rootnamespace$.App_Start.EventStreamStart), "Start")]

namespace $rootnamespace$.App_Start {
    public static class EventStreamStart {
        public static void Start() {
			SignalR.Infrastructure.DependencyResolver.Register(typeof (SignalR.IClientIdFactory), () => new EventStreamStart.ClientIdFactory());
        }

		public class ClientIdFactory : SignalR.IClientIdFactory
		{
			public string CreateClientId(System.Web.HttpContextBase context)
			{
				if (context.User.Identity.IsAuthenticated) {
					//You can return anything here however I would
					//recommend returning a Guid as this cookie
					//is plaintext
					return System.Guid.NewGuid().ToString("N");
				}

				//By returning null here we're telling EventStream
				//that this user is not authorized to access the
				//stream
				return null;
			}
		}
    }
}

