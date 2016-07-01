#region MailUp Header
// MailUp
// 
// Solution: FriendshipConsole
// Project: FriendshipConsole
// File: GreetFriendFrom.cs
// 
// Last update: Gianni Bortolo Bossini on 07-06-2016
#endregion
namespace FriendshipConsole.Messages
{
	public class GreetFriendFrom
	{
		public string From { get; private set; }

		public GreetFriendFrom(string from)
		{
			From = from;
		}
	}
}