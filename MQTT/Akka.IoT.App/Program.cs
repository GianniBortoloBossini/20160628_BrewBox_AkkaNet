using System;
using Akka.Actor;
using Akka.IoT.App.Actors;
using Akka.IoT.Infrastructure.Messages;

namespace Akka.IoT.App
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Akka.IoT Application started...");
			Console.WriteLine();

			// Creazione actor system
			Console.Write("Creating actor system... ");
			ActorSystem actorSystemApplication = ActorSystem.Create(ActorSystemReferences.ActorSystems.ApplicationActorSystemName);
			Console.WriteLine("CREATED");

			// Creazione attore PlantCoordinatorActor ed avvio
			Console.Write("Starting PlantCoordinator... ");
			IActorRef plantSupervisorActor = 
				actorSystemApplication.ActorOf( Props.Create<PlantCoordinatorActor>(), ActorSystemReferences.ActorNames.PlantCoordinatorActorName );
			plantSupervisorActor.Tell(new StartPlantCoordinator());
			Console.WriteLine("STARTED");

			Console.WriteLine();
			Console.WriteLine("Press ENTER to terminate...");
			Console.ReadLine();

			// Arresto attore PlantCoordinatorActor
			Console.Write("Stopping PlantCoordinator... ");
			plantSupervisorActor.Tell(new StopPlantSupervisor());
			Console.WriteLine("STOPPED");

			// Arresto actor system
			actorSystemApplication.Terminate();
			actorSystemApplication.WhenTerminated.Wait();
		}
	}
}
