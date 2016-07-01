#region MailUp Header
// MailUp
// 
// Solution: Akka.IoT
// Project: Akka.IoT.MQTT
// File: MqttEnvironmentActor.cs
// 
// Last update: Gianni Bortolo Bossini on 12-06-2016
#endregion

using System.Threading.Tasks;
using Akka.Actor;
using Akka.IoT.Infrastructure.Messages;
using Akka.IoT.MQTT.Infrastructure.Helpers;
using Akka.IoT.MQTT.Infrastructure.Messages;

namespace Akka.IoT.MQTT.Actors
{
	public class MqttEnvironmentActor : ReceiveActor
	{
		public MqttEnvironmentActor()
		{
			Become(Stopped);
		}

		private void Started()
		{
			Context.ActorSelection(MQTTHelper.ActorPaths.BrokerActorPath).Tell(new StartMQTTBroker());
			Context.ActorSelection(MQTTHelper.ActorPaths.TranslatorActorPath).Tell(new StartTranslator());

			Receive<StopMqtt>(msg => OnStopMqtt(msg));
		}

		private void Stopped()
		{
			Context.ActorSelection(MQTTHelper.ActorPaths.BrokerActorPath).Tell(new StopMQTTBroker());
			Context.ActorSelection(MQTTHelper.ActorPaths.TranslatorActorPath).Tell(new StopTranslator());

			Receive<StartMqtt>( msg => OnStartMqtt( msg ) );
		}

		private void OnStartMqtt( StartMqtt msg )
		{
			Become(Started);
		}

		private void OnStopMqtt(StopMqtt msg)
		{
			Become(Stopped);
		}
	}
}