#region MailUp Header
// MailUp
// 
// Solution: FriendshipConsole
// Project: FriendshipConsole
// File: RememberFriendsError.cs
// 
// Last update: Gianni Bortolo Bossini on 06-06-2016
#endregion
namespace FriendshipConsole.Messages
{
	public class RememberFriendsError
	{
		public string OwnerName { get; private set; }

		public RememberFriendsError( string ownerName )
		{
			OwnerName = ownerName;
		}
	}
}