SignalR.EventStream
-------------------
SignalR.EventStream was born out of a desire to monitor my websites
for activities that I was interested in. User signups, logins, errors,
anything really. SignalR from David Fowler and Damian Edwards has made
that nice and easy.


Usage
-----
Here's a sample on how to use EventStream to send events to authorized users.

    public class HomeController 
	{

		public IEventStream eventStream;

		//Dependency Injection
		public HomeController(IEventStream stream) 
		{
			eventStream = stream;
		}

		//No Dependency Injection
		public HomeController() : this(new EventStream())
		{
		}

		public ActionResult Login() 
		{
			stream.Send("User has logged in.");

			return View();
		}

		public ActionResult Purchase() 
		{
			stream.Send(new PurchaseObject { 
				Name = "Some product",
				Price = 99.99,
				Quantity = 23
			});

			return View();
		}

		public ActionResult StreamAnonymous() 
		{
			stream.Send("error", new {
				Error = "This is an error - or something",
				Description = "Someone has attempted to stream an anonymous object",
				Details = "This is ok, but for anonymous objects you must pass a 'type'"
			});				

			return View();
		}
	}