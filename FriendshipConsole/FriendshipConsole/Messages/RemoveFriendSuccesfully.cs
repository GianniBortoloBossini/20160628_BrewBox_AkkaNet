#region MailUp Header
// MailUp
// 
// Solution: FriendshipConsole
// Project: FriendshipConsole
// File: RemoveFriendSuccesfully.cs
// 
// Last update: Gianni Bortolo Bossini on 06-06-2016
#endregion
namespace FriendshipConsole.Messages
{
	public class RemoveFriendSuccesfully
	{
		public RemoveFriendSuccesfully( string friendName, string ownerName )
		{
			OwnerName = ownerName;
			FriendName = friendName;
		}

		public string FriendName { get; private set; }
		public string OwnerName { get; private set; }
	}
}