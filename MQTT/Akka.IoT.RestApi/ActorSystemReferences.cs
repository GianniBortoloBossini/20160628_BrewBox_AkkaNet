#region MailUp Header
// MailUp
// 
// Solution: Akka.IoT
// Project: Akka.IoT.RestApi
// File: ActorSystemReferences.cs
// 
// Last update: Gianni Bortolo Bossini on 16-06-2016
#endregion

using System.Configuration;
using Akka.Actor;

namespace Akka.IoT.RestApi
{
	public class ActorSystemReferences
	{
		public class ActorSystems
		{
			static ActorSystems()
			{
				RestApiActorSystemName = "ActorSystem-Akka-IoT-RestApi";
				PlantCoordinatorActorSystemName = "ActorSystem-Akka-IoT-App";
			}

			public static string PlantCoordinatorActorSystemName { get; private set; }
			public static string RestApiActorSystemName { get; private set; }

			public static ActorSystem RestApiActorSystem { get; set; }
		}

		public class Actors
		{
			static Actors()
			{
				RestApiActorName = "RestApiActor";
				CameraShotsActorName = "CameraShotsActor";
				PlantCoordinatorActorName = "PlantCoordinatorActor";
			}

			public static string PlantCoordinatorActorName { get; set; }
			public static string RestApiActorName { get; private set; }
			public static string CameraShotsActorName { get; private set; }

			public static ActorSelection PlantCoordinatorActor { get; set; }
		}

		public class ActorPaths
		{
			static ActorPaths()
			{
				// PlantCoordinator reference
				string plantCoordinatorActorAddress = ConfigurationManager.AppSettings.Get("plantCoordinatorActorAddress");
				int plantCoordinatorActorPort = int.Parse(ConfigurationManager.AppSettings.Get("plantCoordinatorActorPort"));
				string plantCoordinatorActorSystemName = ActorSystems.PlantCoordinatorActorSystemName;
				string plantCoordinatorActorName = Actors.PlantCoordinatorActorName;
				PlantCoordinatorActorPath 
					= string.Format("akka.tcp://{0}@{1}:{2}/user/{3}", 
									plantCoordinatorActorSystemName, 
									plantCoordinatorActorAddress,
									plantCoordinatorActorPort,
									plantCoordinatorActorName);	
			}
			public static string PlantCoordinatorActorPath { get; private set; }
		}
	}
}