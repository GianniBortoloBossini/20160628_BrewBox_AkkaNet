#region MailUp Header
// MailUp
// 
// Solution: FriendshipConsole
// Project: FriendshipConsole
// File: AddNewFriendError.cs
// 
// Last update: Gianni Bortolo Bossini on 05-06-2016
#endregion
namespace FriendshipConsole.Messages
{
	public class AddFriendError
	{
		public AddFriendError(string friendName, string ownerName)
		{
			OwnerName = ownerName;
			FriendName = friendName;
		}

		public string OwnerName { get; private set; }
		public string FriendName { get; private set; }
	}
}