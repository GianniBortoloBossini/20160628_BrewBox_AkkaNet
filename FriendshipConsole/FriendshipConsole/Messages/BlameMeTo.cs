#region MailUp Header
// MailUp
// 
// Solution: FriendshipConsole
// Project: FriendshipConsole
// File: BlameMeTo.cs
// 
// Last update: Gianni Bortolo Bossini on 07-06-2016
#endregion
namespace FriendshipConsole.Messages
{
	public class BlameMeTo
	{
		public string FriendToBlame { get; private set; }

		public BlameMeTo(string friendToBlame)
		{
			FriendToBlame = friendToBlame;
		}
	}
}