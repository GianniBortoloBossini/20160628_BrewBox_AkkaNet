using System;
using Akka.Actor;
using Akka.IoT.Broker.Actors;
using Akka.IoT.MQTT.Infrastructure.Helpers;
using Akka.IoT.MQTT.Infrastructure.Messages;

namespace Akka.IoT.MQTT.Broker
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("MQTT Broker started...");
			Console.WriteLine();
			ActorSystem actorSystemBroker = ActorSystem.Create(MQTTHelper.ActorSystems.BrokerActorSystem);

			IActorRef brokerActor = actorSystemBroker.ActorOf(Props.Create<MqttBrokerActor>(), MQTTHelper.ActorNames.BrokerActor);

			Console.ReadLine();

			actorSystemBroker.WhenTerminated.Wait();
		}
	}
}
