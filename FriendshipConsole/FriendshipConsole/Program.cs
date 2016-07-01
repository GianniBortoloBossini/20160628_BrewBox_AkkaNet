using Akka.Actor;
using Akka.Configuration;
using FriendshipConsole.Actors;

namespace FriendshipConsole
{
	internal class Program
	{
		static void Main(string[] args)
		{
			// Starting from Akka.NET v1.5, default Newtonsoft.Json serializer will be replaced in the favor of Wire.
			// Install-Package Akka.Serialization.Wire -Pre
			Config config = @"akka {
			  actor {
				serializers {
				  wire = ""Akka.Serialization.WireSerializer, Akka.Serialization.Wire""
				}
				serialization-bindings {
				  ""System.Object"" = wire
				}
			  }
			}";

			ActorSystem actorSystem = ActorSystem.Create("FriendshipConsoleActorSystem", config);
			actorSystem.ActorOf(Props.Create<ApplicationActor>(), "Application");

			actorSystem.WhenTerminated.Wait();
		}

	//static void Main(string[] args)
		//{
		//	// Intro
		//	string myName = ShowIntro();

		//	// Setup
		//	bool isFirstAccess = true;
		//	Friends friends = new Friends();

		//	// Create chain of responsability
		//	Handler addNewFriend = new AddNewFriend(friends, myName);
		//	Handler removeExistingFriend = new RemoveExistingFriend(friends, myName);
		//	Handler rememberFriends = new RememberFriends(friends, myName);
		//	Handler greetFriend = new GreetFriend(friends, myName);
		//	Handler greetAllFriends = new GreetAllFriends(friends, myName);
		//	Handler forwardMyGreetings = new ForwardMyGreetings(friends, myName);
		//	Handler killYourFriends = new KillYourFriends(friends, myName);
		//	Handler exit = new Exit();
		//	addNewFriend.SetSuccessor( removeExistingFriend );
		//	removeExistingFriend.SetSuccessor(rememberFriends);
		//	rememberFriends.SetSuccessor(greetFriend);
		//	greetFriend.SetSuccessor(greetAllFriends);
		//	greetAllFriends.SetSuccessor(forwardMyGreetings);
		//	forwardMyGreetings.SetSuccessor(killYourFriends);
		//	killYourFriends.SetSuccessor(exit);

		//	while ( true )
		//	{
		//		if ( !isFirstAccess )
		//			ShowTitle();
		//		else
		//			isFirstAccess = false;

		//		// Menu
		//		string yourChoice = ShowMenu(myName);

		//		byte choiceIndex = 0;
		//		if ( byte.TryParse( yourChoice, out choiceIndex ) && (choiceIndex >= Choices.MinChoiceValue && choiceIndex <= Choices.MaxChoiceValue) )
		//		{
		//			try
		//			{
		//				addNewFriend.HandleRequest(choiceIndex);
		//			}
		//			catch ( ExitException )
		//			{
		//				break;
		//			}
		//		}
		//		else
		//		{
		//			ShowWrongChoice( myName );
		//		}
		//	}

		//	Console.WriteLine();
		//	Console.WriteLine("Bye bye!");
		//	Console.ReadLine();
		//}

		//private static void ShowWrongChoice( string myName )
		//{
		//	Console.ForegroundColor = ConsoleColor.Red;
		//	Console.WriteLine("Ops! You made a wrong choice.");
		//	Console.Write("Please, pay attention ");
		//	Console.ForegroundColor = ConsoleColor.Green;
		//	Console.Write(myName);
		//	Console.WriteLine("!");
		//	Console.ResetColor();
		//	Console.WriteLine();
		//	Console.WriteLine("Press any key to continue...");
		//	Console.ReadLine();
		//	Console.Clear();
		//}

		//private static string ShowMenu(string myName)
		//{

		//	string yourChoice = Console.ReadLine();

		//	return yourChoice;
		//}

		//private static void ShowTitle()
		//{

		//}
	}
}
