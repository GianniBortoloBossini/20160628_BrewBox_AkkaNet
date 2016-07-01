#region MailUp Header
// MailUp
// 
// Solution: Akka.IoT
// Project: Akka.IoT.Infrastructure
// File: ActorSystemsNameHelper.cs
// 
// Last update: Gianni Bortolo Bossini on 12-06-2016
#endregion
namespace Akka.IoT.MQTT.Infrastructure.Helpers
{
	public class MQTTHelper
	{
		public class ActorSystems
		{
			public static string BrokerActorSystem { get { return "actorSystemBroker"; } }
			public static string MessageTranslatorActorSystem { get { return "actorSystemMessageTranslator"; } }
		}

		public class ActorPaths
		{
			public static string BrokerActorPath = string.Format("akka.tcp://{0}@localhost:8091/user/{1}", ActorSystems.BrokerActorSystem, ActorNames.BrokerActor);
			public static string TranslatorActorPath = string.Format("akka.tcp://{0}@localhost:8092/user/{1}", ActorSystems.MessageTranslatorActorSystem, ActorNames.TranslatorActor);
		}

		public class ActorNames
		{
			public static string BrokerActor = "mqttBrokerActor";
			public static string TranslatorActor = "mqttTranslatorActor";
		}
	}
}