#region MailUp Header
// MailUp
// 
// Solution: FriendshipConsole
// Project: FriendshipConsole
// File: RemoveFriend.cs
// 
// Last update: Gianni Bortolo Bossini on 06-06-2016
#endregion
namespace FriendshipConsole.Messages
{
	public class RemoveFriend
	{
		public RemoveFriend( string friendName )
		{
			FriendName = friendName;
		}

		public string FriendName { get; private set; }
	}
}