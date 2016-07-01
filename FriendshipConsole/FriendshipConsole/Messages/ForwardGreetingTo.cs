#region MailUp Header
// MailUp
// 
// Solution: FriendshipConsole
// Project: FriendshipConsole
// File: ForwardGreeting.cs
// 
// Last update: Gianni Bortolo Bossini on 07-06-2016
#endregion
namespace FriendshipConsole.Messages
{
	public class ForwardGreetingTo
	{
		public string ForwarderFriend { get; private set; }
		public string TargetFriend { get; private set; }

		public ForwardGreetingTo(string forwarderFriend, string targetFriend)
		{
			ForwarderFriend = forwarderFriend;
			TargetFriend = targetFriend;
		}
	}
}