using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendshipConsole.Chain
{
	public class RemoveExistingFriend : Handler
	{
		private readonly Friends _friends;
		private readonly string _myName;

		public RemoveExistingFriend(Friends friends, string myName)
		{
			_friends = friends;
			_myName = myName;
		}

		public override void HandleRequest(int choice)
		{
			if (choice == 2)
			{
				string friendName = Friend.GetExistingFriendName("Insert here the name of the friend you want to leave: ");
				if (_friends.LeaveFriend(friendName) > 0)
				{
					Console.WriteLine();
					Console.ForegroundColor = ConsoleColor.Magenta;
					Console.Write( "I'm sorry " );
					Console.ForegroundColor = ConsoleColor.Green;
					Console.Write(friendName);
					Console.ForegroundColor = ConsoleColor.Magenta;
					Console.WriteLine(", but we cannot be friend!");

					Console.ResetColor();
					Console.WriteLine();
					Console.WriteLine("Press any key to continue...");
					Console.ReadLine();

					Console.Clear();
				}
				else
				{
					Console.WriteLine();
					Console.ForegroundColor = ConsoleColor.Red;
					Console.Write("Ops! You haven't got any friend named ");
					Console.ForegroundColor = ConsoleColor.Green;
					Console.Write(friendName);
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine(".");
					Console.WriteLine();
					Console.Write("Please, pay attention ");
					Console.ForegroundColor = ConsoleColor.Green;
					Console.Write(_myName);
					Console.WriteLine("!");
					Console.ResetColor();
					Console.WriteLine();
					Console.WriteLine("Press any key to continue...");
					Console.ReadLine();
					Console.Clear();
				}
			}
			else if (Successor != null)
			{
				Successor.HandleRequest(choice);
			}
		}
	}
}
