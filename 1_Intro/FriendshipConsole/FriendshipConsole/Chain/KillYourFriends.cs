#region MailUp Header
// MailUp
// 
// Solution: FriendshipConsole
// Project: FriendshipConsole
// File: KillYourFriends.cs
// 
// Last update: Gianni Bortolo Bossini on 04-06-2016
#endregion

using System;
using System.Linq;

namespace FriendshipConsole.Chain
{
	public class KillYourFriends : Handler
	{
		private readonly Friends _friends;
		private readonly string _myName;

		public KillYourFriends(Friends friends, string myName)
		{
			_friends = friends;
			_myName = myName;
		}

		public override void HandleRequest( int choice )
		{
			if ( choice == 6 )
			{
				if ( _friends.Any() )
				{
					while (true)
					{
						Friend friend = _friends.FirstOrDefault();
						Console.WriteLine();
						if (friend == null)
						{
							Console.ForegroundColor = ConsoleColor.Magenta;
							Console.WriteLine("Now you are ALONE!!! Are your happy?!?!");
							Console.ResetColor();
							Console.WriteLine();
							Console.WriteLine("Press any key to continue...");
							Console.ReadLine();
							Console.Clear();
							return;
						}
						else
						{
							_friends.Remove( friend );
							Console.ForegroundColor = ConsoleColor.Magenta;
							Console.Write("F*@k you, ");
							Console.ForegroundColor = ConsoleColor.Green;
							Console.Write(friend.Name);
							Console.ForegroundColor = ConsoleColor.Magenta;
							Console.Write("!!!");
						}
					}
				}
				else
				{
					Console.WriteLine();
					Console.ForegroundColor = ConsoleColor.Red;
					Console.Write("Ops! You are ALONE!!!");
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