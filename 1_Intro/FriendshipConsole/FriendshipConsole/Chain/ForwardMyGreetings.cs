#region MailUp Header
// MailUp
// 
// Solution: FriendshipConsole
// Project: FriendshipConsole
// File: ForwardMyGreetings.cs
// 
// Last update: Gianni Bortolo Bossini on 04-06-2016
#endregion

using System;
using System.Linq;

namespace FriendshipConsole.Chain
{
	public class ForwardMyGreetings : Handler
	{
		private readonly Friends _friends;
		private readonly string _myName;

		public ForwardMyGreetings( Friends friends, string myName )
		{
			_friends = friends;
			_myName = myName;
		}

		public override void HandleRequest(int choice)
		{
			if (choice == 5)
			{
				string friendName = Friend.GetExistingFriendName("Insert the name of the friend that should forward your greetings: ");

				if (_friends.Any(f => f.Name == friendName))
				{
					Console.WriteLine();
					Console.ForegroundColor = ConsoleColor.Magenta;
					Console.Write("Please ");
					Console.ForegroundColor = ConsoleColor.Green;
					Console.Write(friendName);
					Console.ForegroundColor = ConsoleColor.Magenta;
					Console.Write(", forward my greetings to: ");
					Console.ResetColor();
					string targetFriend = Console.ReadLine();

					if (_friends.Any(f => f.Name == targetFriend))
					{
						Console.WriteLine();
						Console.ForegroundColor = ConsoleColor.Magenta;
						Console.Write("Hey ");
						Console.ForegroundColor = ConsoleColor.Green;
						Console.Write(_myName);
						Console.ForegroundColor = ConsoleColor.Magenta;
						Console.Write(", I forward your greetings to ");
						Console.ForegroundColor = ConsoleColor.Green;
						Console.Write(targetFriend);
						Console.ForegroundColor = ConsoleColor.Magenta;
						Console.Write("!");

						_friends.First(f => f.Name == targetFriend).Greetings(_myName);

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
						Console.Write(targetFriend);
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