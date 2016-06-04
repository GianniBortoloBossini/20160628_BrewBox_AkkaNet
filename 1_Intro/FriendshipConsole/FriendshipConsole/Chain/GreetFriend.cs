#region MailUp Header
// MailUp
// 
// Solution: FriendshipConsole
// Project: FriendshipConsole
// File: GreetFriend.cs
// 
// Last update: Gianni Bortolo Bossini on 03-06-2016
#endregion

using System;
using System.Linq;

namespace FriendshipConsole.Chain
{
	public class GreetFriend : Handler
	{
		private Friends _friends;
		private string _myName;

		public GreetFriend(Friends friends, string myName)
		{
			_friends = friends;
			_myName = myName;
		}

		public override void HandleRequest(int choice)
		{
			if (choice == 3)
			{
				string friendName = Friend.GetExistingFriendName("Insert here the name of the friend you want to greet: ");
				if (_friends.Any(f => f.Name == friendName))
				{
					_friends.First(f => f.Name == friendName).Greetings(_myName);

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