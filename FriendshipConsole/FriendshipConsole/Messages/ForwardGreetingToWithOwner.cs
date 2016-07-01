#region MailUp Header
// MailUp
// 
// Solution: FriendshipConsole
// Project: FriendshipConsole
// File: ForwardGreetingToWithOwner.cs
// 
// Last update: Gianni Bortolo Bossini on 07-06-2016
#endregion
namespace FriendshipConsole.Messages
{
	public class ForwardGreetingToWithOwner
	{
		public string OwnerName { get; private set; }
		public string ForwarderFriend { get; private set; }
		public string TargetFriend { get; private set; }

		public ForwardGreetingToWithOwner(string ownerName, string forwarderFriend, string targetFriend)
		{
			OwnerName = ownerName;
			ForwarderFriend = forwarderFriend;
			TargetFriend = targetFriend;
		}
	}
}