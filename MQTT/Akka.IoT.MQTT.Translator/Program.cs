using System;
using Akka.Actor;
using Akka.IoT.MQTT.Infrastructure.Helpers;
using Akka.IoT.MQTT.Translator.Actors;

namespace Akka.IoT.MQTT.Translator
{
	class Program
	{
		static void Main(string[] args)
		{
			ActorSystem actorSystemMessageTranslator = ActorSystem.Create( MQTTHelper.ActorSystems.MessageTranslatorActorSystem );

			IActorRef translatorActor = actorSystemMessageTranslator.ActorOf( Props.Create<MqttTranslatorActor>(), MQTTHelper.ActorNames.TranslatorActor );

			Console.WriteLine("Press ENTER to quit..");
			Console.ReadLine();

			actorSystemMessageTranslator.Terminate();

			actorSystemMessageTranslator.WhenTerminated.Wait();
		}
	}
}
