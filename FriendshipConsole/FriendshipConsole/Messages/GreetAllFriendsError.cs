#region MailUp Header
// MailUp
// 
// Solution: FriendshipConsole
// Project: FriendshipConsole
// File: GreetAllFriendsError.cs
// 
// Last update: Gianni Bortolo Bossini on 07-06-2016
#endregion
namespace FriendshipConsole.Messages
{
	public class GreetAllFriendsError
	{
		public GreetAllFriendsError( string ownerName )
		{
			OwnerName = ownerName;
		}

		public string OwnerName { get; private set; }
	}
}