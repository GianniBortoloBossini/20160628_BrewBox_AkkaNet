#region MailUp Header
// MailUp
// 
// Solution: FriendshipConsole
// Project: FriendshipConsole
// File: AddNewFriendSuccesfully.cs
// 
// Last update: Gianni Bortolo Bossini on 05-06-2016
#endregion
namespace FriendshipConsole.Messages
{
	public class AddFriendSuccesfully
	{
		public AddFriendSuccesfully( string friendName, string ownerName )
		{
			OwnerName = ownerName;
			FriendName = friendName;
		}

		public string FriendName { get; private set; }
		public string OwnerName { get; private set; }
	}
}