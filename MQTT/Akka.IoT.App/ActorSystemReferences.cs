#region MailUp Header
// MailUp
// 
// Solution: Akka.IoT
// Project: Akka.IoT.App
// File: ActorSystemReferences.cs
// 
// Last update: Gianni Bortolo Bossini on 16-06-2016
#endregion
namespace Akka.IoT.App
{
	public class ActorSystemReferences
	{
		public class ActorSystems
		{
			static ActorSystems()
			{
				ApplicationActorSystemName = "ActorSystem-Akka-IoT-App";
			}

			public static string ApplicationActorSystemName { get; private set; }
		}

		public class ActorNames
		{
			static ActorNames()
			{
				SnapshotCoordinatorActorName = "SnapshotCoordinatorActor";
				SnapshotActorNamePrefix = "SnapshotActor_";
				PlantCoordinatorActorName = "PlantCoordinatorActor";
				PlantCoordinatorMqttTranslatorActorName = "PlantCoordinatorMqttTranslatorActor";
				DeviceMqttTranslatorActorNamePrefix = "DeviceMqttTranslatorActor_";
				DeviceActorNamePrefix = "DeviceActor_";
			}

			public static string PlantCoordinatorActorName { get; private set; }
			public static string PlantCoordinatorMqttTranslatorActorName { get; private set; }
			public static string DeviceMqttTranslatorActorNamePrefix { get; private set; }
			public static string DeviceActorNamePrefix { get; private set; }
			public static string SnapshotCoordinatorActorName { get; private set; }
			public static string SnapshotActorNamePrefix { get; private set; }
		}
	}
}