#region MailUp Header
// MailUp
// 
// Solution: FriendshipConsole
// Project: FriendshipConsole
// File: ShowWrongChoiceMessage.cs
// 
// Last update: Gianni Bortolo Bossini on 05-06-2016
#endregion
namespace FriendshipConsole.Messages.Command
{
	public class ShowWrongChoiceMessage
	{
		public string OwnerName { get; private set; }

		public ShowWrongChoiceMessage(string ownerName)
		{
			OwnerName = ownerName;
		}
	}
}