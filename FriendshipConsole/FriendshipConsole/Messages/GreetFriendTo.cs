#region MailUp Header
// MailUp
// 
// Solution: FriendshipConsole
// Project: FriendshipConsole
// File: GreetFriendTo.cs
// 
// Last update: Gianni Bortolo Bossini on 07-06-2016
#endregion
namespace FriendshipConsole.Messages
{
	public class GreetFriendTo
	{
		public string To { get; private set; }
		public string From { get; private set; }

		public GreetFriendTo(string to, string from)
		{
			To = to;
			From = @from;
		}
	}
}