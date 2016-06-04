using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FriendshipConsole.Chain;

namespace FriendshipConsole
{
	class Program
	{
		static void Main(string[] args)
		{
			// Intro
			string myName = ShowIntro();

			// Setup
			bool isFirstAccess = true;
			Friends friends = new Friends();

			// Create chain of responsability
			Handler addNewFriend = new AddNewFriend(friends, myName);
			Handler removeExistingFriend = new RemoveExistingFriend(friends, myName);
			Handler rememberFriends = new RememberFriends(friends, myName);
			Handler greetFriend = new GreetFriend(friends, myName);
			Handler greetAllFriends = new GreetAllFriends(friends, myName);
			Handler forwardMyGreetings = new ForwardMyGreetings(friends, myName);
			Handler killYourFriends = new KillYourFriends(friends, myName);
			Handler exit = new Exit();
			addNewFriend.SetSuccessor( removeExistingFriend );
			removeExistingFriend.SetSuccessor(rememberFriends);
			rememberFriends.SetSuccessor(greetFriend);
			greetFriend.SetSuccessor(greetAllFriends);
			greetAllFriends.SetSuccessor(forwardMyGreetings);
			forwardMyGreetings.SetSuccessor(killYourFriends);
			killYourFriends.SetSuccessor(exit);

			while ( true )
			{
				if ( !isFirstAccess )
					ShowTitle();
				else
					isFirstAccess = false;

				// Menu
				string yourChoice = ShowMenu(myName);

				byte choiceIndex = 0;
				if ( byte.TryParse( yourChoice, out choiceIndex ) && (choiceIndex >= Choices.MinChoiceValue && choiceIndex <= Choices.MaxChoiceValue) )
				{
					try
					{
						addNewFriend.HandleRequest(choiceIndex);
					}
					catch ( ExitException )
					{
						break;
					}
				}
				else
				{
					ShowWrongChoice( myName );
				}
			}

			Console.WriteLine();
			Console.WriteLine("Bye bye!");
			Console.ReadLine();
		}

		private static void ShowWrongChoice( string myName )
		{
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine("Ops! You made a wrong choice.");
			Console.Write("Please, pay attention ");
			Console.ForegroundColor = ConsoleColor.Green;
			Console.Write(myName);
			Console.WriteLine("!");
			Console.ResetColor();
			Console.WriteLine();
			Console.WriteLine("Press any key to continue...");
			Console.ReadLine();
			Console.Clear();
		}

		private static string ShowMenu(string myName)
		{
			Console.ForegroundColor = ConsoleColor.Green;
			Console.Write(myName);
			Console.ResetColor();
			Console.WriteLine(", please tell me what you want to do:");
			foreach ( KeyValuePair<int, string> choiceItem in Choices.List )
			{
				Console.WriteLine("{0} - {1}", choiceItem.Key, choiceItem.Value);
			}

			Console.WriteLine();
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.Write("Make your choice: ");
			Console.ResetColor();
			string yourChoice = Console.ReadLine();

			return yourChoice;
		}

		private static void ShowTitle()
		{
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.WriteLine("Friendship console 2.0!");
			Console.ResetColor();
			Console.WriteLine();
		}

		private static string ShowIntro()
		{
			// Intro
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.WriteLine( "Welcome to Friendship console 2.0!" );
			Console.ResetColor();
			Console.WriteLine();

			Console.ForegroundColor = ConsoleColor.Blue;
			Console.Write("Please insert your name or nickname: ");
			Console.ResetColor();
			string myName = Console.ReadLine();
			Console.WriteLine();

			Console.Write("Nice to meet you ");
			Console.ForegroundColor = ConsoleColor.Green;
			Console.Write(myName);
			Console.ResetColor();
			Console.WriteLine(".");
			Console.WriteLine();

			return myName;
		}
	}
}
