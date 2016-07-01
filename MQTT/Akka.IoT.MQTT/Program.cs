using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using Akka.IoT.Infrastructure.Helpers;
using Akka.IoT.MQTT.Actors;

namespace Akka.IoT.MQTT
{
	class Program
	{
		static void Main(string[] args)
		{
			ActorSystem actorSystemMqttEnvironment = ActorSystem.Create( AppHelper.ActorSystems.MqttEnvironmentActorSystem );
			actorSystemMqttEnvironment.ActorOf( Props.Create<MqttEnvironmentActor>(), AppHelper.ActorNames.MqttEnvironmentActor );

			Console.WriteLine();
			Console.WriteLine("Press ENTER to terminate...");
			Console.ReadLine();

			actorSystemMqttEnvironment.Terminate();

			actorSystemMqttEnvironment.WhenTerminated.Wait();
		}
	}
}
