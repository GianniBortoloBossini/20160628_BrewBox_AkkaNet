using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendshipConsole.Chain
{
	public class AddNewFriend : Handler
	{
		private readonly Friends _friends;
		private readonly string _myName;

		public AddNewFriend(Friends friends, string myName)
		{
			_friends = friends;
			_myName = myName;
		}

		public override void HandleRequest(int choice)
		{
			if (choice == 1)
			{
				string newFriendName = Friend.GetNewFriendName();
				if ( !_friends.Any( f => f.Name == newFriendName ) )
				{
					Friend friend = _friends.AddNewFriend(newFriendName);
					friend.NiceToMeetYou(_myName);	
				}
				else
				{
					Console.WriteLine();
					Console.ForegroundColor = ConsoleColor.Red;
					Console.Write("Ops! You have already got a friend named ");
					Console.ForegroundColor = ConsoleColor.Green;
					Console.Write(newFriendName);	
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("!");
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
