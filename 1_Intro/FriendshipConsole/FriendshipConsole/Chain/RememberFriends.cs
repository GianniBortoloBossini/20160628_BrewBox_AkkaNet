#region MailUp Header
// MailUp
// 
// Solution: FriendshipConsole
// Project: FriendshipConsole
// File: RememberFriends.cs
// 
// Last update: Gianni Bortolo Bossini on 03-06-2016
#endregion

using System;
using System.Linq;

namespace FriendshipConsole.Chain
{
	public class RememberFriends : Handler
	{
		private readonly Friends _friends;
		private readonly string _myName;

		public RememberFriends(Friends friends, string myName)
		{
			_friends = friends;
			_myName = myName;
		}

		public override void HandleRequest(int choice)
		{
			if (choice == 7)
			{
				if ( _friends.Any() )
				{
					Console.WriteLine();
					Console.ForegroundColor = ConsoleColor.Magenta;
					Console.Write( "Hey " );
					Console.ForegroundColor = ConsoleColor.Green;
					Console.Write( _myName );
					Console.ForegroundColor = ConsoleColor.Magenta;
					Console.WriteLine( ", do you remember your friends?" );
					Console.WriteLine();

					foreach ( Friend friend in _friends )
					{
						Console.WriteLine( friend.Name );
					}
				}
				else
				{
					Console.WriteLine();
					Console.ForegroundColor = ConsoleColor.Magenta;
					Console.Write("Hey ");
					Console.ForegroundColor = ConsoleColor.Green;
					Console.Write(_myName);
					Console.ForegroundColor = ConsoleColor.Magenta;
					Console.WriteLine(", you are alone, ALONE!!!");
				}

				Console.ResetColor();
				Console.WriteLine();
				Console.WriteLine("Press any key to continue...");
				Console.ReadLine();

				Console.Clear();
			}
			else if (Successor != null)
			{
				Successor.HandleRequest(choice);
			}
		}
	}
}