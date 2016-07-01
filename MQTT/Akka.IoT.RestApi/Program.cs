using System;
using Akka.Actor;
using Microsoft.Owin.Hosting;

namespace Akka.IoT.RestApi
{
	class Program
	{
		static void Main(string[] args)
		{
			const string baseAddress = "http://localhost:9090/";

			ActorSystemReferences.ActorSystems.RestApiActorSystem = 
				ActorSystem.Create(ActorSystemReferences.ActorSystems.RestApiActorSystemName);
			
			ActorSystemReferences.Actors.PlantCoordinatorActor =
				ActorSystemReferences.ActorSystems.RestApiActorSystem.ActorSelection(
					ActorSystemReferences.ActorPaths.PlantCoordinatorActorPath );
			
			using (WebApp.Start<Startup>(baseAddress))
			{
				Console.WriteLine("Press [enter] to quit...");
				Console.ReadLine();
			}

			ActorSystemReferences.ActorSystems.RestApiActorSystem.Terminate().Wait();

			Console.WriteLine("Bye bye...");
		}
	}
}
