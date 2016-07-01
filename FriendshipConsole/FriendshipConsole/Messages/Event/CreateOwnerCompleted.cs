#region MailUp Header
// MailUp
// 
// Solution: FriendshipConsole
// Project: FriendshipConsole
// File: TypeOwnerNameCompleted.cs
// 
// Last update: Gianni Bortolo Bossini on 05-06-2016
#endregion
namespace FriendshipConsole.Messages.Event
{
	public class CreateOwnerCompleted
	{
		public string OwnerName { get; private set; }

		public CreateOwnerCompleted(string ownerName)
		{
			OwnerName = ownerName;
		}
	}
}