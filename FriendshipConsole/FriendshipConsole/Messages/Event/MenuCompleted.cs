#region MailUp Header
// MailUp
// 
// Solution: FriendshipConsole
// Project: FriendshipConsole
// File: MenuCompleted.cs
// 
// Last update: Gianni Bortolo Bossini on 05-06-2016
#endregion
namespace FriendshipConsole.Messages.Event
{
	public class MenuCompleted
	{
		public string Selection { get; private set; }

		public MenuCompleted(string selection)
		{
			Selection = selection;
		} 
	}
}