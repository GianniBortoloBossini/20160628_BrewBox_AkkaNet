#region MailUp Header
// MailUp
// 
// Solution: Akka.IoT
// Project: Akka.IoT.Infrastructure
// File: AppHelper.cs
// 
// Last update: Gianni Bortolo Bossini on 12-06-2016
#endregion
namespace Akka.IoT.Infrastructure.Helpers
{
	public class AppHelper
	{
		public class ActorSystems
		{
			public static string BrokerActorSystem { get { return "actorSystemBroker"; } }
			//public static string MqttEnvironmentActorSystem { get { return "actorSystemMqttEnvironment"; } }
			public static string ApplicationActorSystem { get { return "actorSystemApplication"; } }
		}

		public class ActorPaths
		{
			//public static string MqttActorPath = string.Format("akka.tcp://{0}@localhost:8090/user/{1}", ActorSystems.MqttEnvironmentActorSystem, ActorNames.MqttEnvironmentActor);
			public static string BrokerActorPath = string.Format("akka.tcp://{0}@localhost:8091/user/{1}", ActorSystems.BrokerActorSystem, ActorNames.BrokerActor);
		}

		public class ActorNames
		{
			public static string BrokerActor = "mqttBrokerActor";
			//public static string MqttEnvironmentActor = "mqttEnvironmentActor";
		}
	}
}