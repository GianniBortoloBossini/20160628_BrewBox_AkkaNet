#region MailUp Header
// MailUp
// 
// Solution: FriendshipConsole
// Project: FriendshipConsole
// File: ForwardGreetingAskTo.cs
// 
// Last update: Gianni Bortolo Bossini on 07-06-2016
#endregion
namespace FriendshipConsole.Messages
{
	public class ForwardGreetingAskTo
	{
		public string ForwarderFriendName { get; private set; }

		public ForwardGreetingAskTo( string forwarderFriendName )
		{
			ForwarderFriendName = forwarderFriendName;
		}
	}
}