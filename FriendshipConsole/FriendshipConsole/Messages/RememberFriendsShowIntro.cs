#region MailUp Header
// MailUp
// 
// Solution: FriendshipConsole
// Project: FriendshipConsole
// File: RememberFriendsShowIntro.cs
// 
// Last update: Gianni Bortolo Bossini on 06-06-2016
#endregion
namespace FriendshipConsole.Messages
{
	public class RememberFriendsShowIntro
	{
		public string OwnerName { get; private set; }

		public RememberFriendsShowIntro( string ownerName )
		{
			OwnerName = ownerName;
		}
	}
}