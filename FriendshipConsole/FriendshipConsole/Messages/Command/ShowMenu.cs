#region MailUp Header
// MailUp
// 
// Solution: FriendshipConsole
// Project: FriendshipConsole
// File: ShowTitle.cs
// 
// Last update: Gianni Bortolo Bossini on 05-06-2016
#endregion
namespace FriendshipConsole.Messages.Command
{
	public class ShowMenu
	{
		public string ApplicationName { get; private set; }
		public string OwnerName { get; private set; }
		public bool ShowTitle { get; private set; }

		public ShowMenu(string applicationName, string ownerName, bool showTitle)
		{
			ApplicationName = applicationName;
			OwnerName = ownerName;
			ShowTitle = showTitle;
		}
	}
}