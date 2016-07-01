#region MailUp Header
// MailUp
// 
// Solution: FriendshipConsole
// Project: FriendshipConsole
// File: RememberFriendsSuccesfully.cs
// 
// Last update: Gianni Bortolo Bossini on 06-06-2016
#endregion

using System.Collections.Generic;

namespace FriendshipConsole.Messages
{
	public class RememberFriendsSuccesfully
	{
		public string Name { get; private set; }
		public IEnumerable<string> Friends { get; private set; }

		public RememberFriendsSuccesfully( string name, IEnumerable<string> friends )
		{
			Name = name;
			Friends = friends;
		}
	}
}