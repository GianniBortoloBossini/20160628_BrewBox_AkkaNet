#region MailUp Header
// MailUp
// 
// Solution: FriendshipConsole
// Project: FriendshipConsole
// File: GreetAllFriends.cs
// 
// Last update: Gianni Bortolo Bossini on 04-06-2016
#endregion

using System;
using System.Linq;

namespace FriendshipConsole.Chain
{
	public class GreetAllFriends : Handler
	{
		private Friends _friends;
		private string _myName;

		public GreetAllFriends(Friends friends, string myName)
		{
			_friends = friends;
			_myName = myName;
		}

		public override void HandleRequest(int choice)
		{
			if (choice == 4)
			{
				if (_friends.Any())
				{
					_friends.ForEach( f =>
									{
										f.Greetings(_myName);
									});

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
					Console.Write("Ops! You haven't got friends!");
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