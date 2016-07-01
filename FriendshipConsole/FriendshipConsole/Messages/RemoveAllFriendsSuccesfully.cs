#region MailUp Header
// MailUp
// 
// Solution: FriendshipConsole
// Project: FriendshipConsole
// File: RemoveAllFriendsSuccesfully.cs
// 
// Last update: Gianni Bortolo Bossini on 07-06-2016
#endregion
namespace FriendshipConsole.Messages
{
	public class RemoveAllFriendsSuccesfully
	{
		public string OwnerName { get; private set; }

		public RemoveAllFriendsSuccesfully( string ownerName )
		{
			OwnerName = ownerName;
		}
	}
}