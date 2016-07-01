#region MailUp Header
// MailUp
// 
// Solution: Akka.IoT
// Project: Akka.IoT.RestApi
// File: Startup.cs
// 
// Last update: Gianni Bortolo Bossini on 15-06-2016
#endregion

using System.Web.Http;
using Owin;

namespace Akka.IoT.RestApi
{
	public class Startup
	{
		// This code configures Web API. The Startup class is specified as a type
		// parameter in the WebApp.Start method.
		public void Configuration(IAppBuilder appBuilder)
		{
			// Configure Web API for self-host. 
			HttpConfiguration config = new HttpConfiguration();
			config.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "api/{controller}/{id}",
				defaults: new { id = RouteParameter.Optional }
			);

			appBuilder.UseWebApi(config);
		}  
	}
}