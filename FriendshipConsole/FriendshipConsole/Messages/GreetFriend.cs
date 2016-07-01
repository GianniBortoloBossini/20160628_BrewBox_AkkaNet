#region MailUp Header
// MailUp
// 
// Solution: FriendshipConsole
// Project: FriendshipConsole
// File: GreetFriend.cs
// 
// Last update: Gianni Bortolo Bossini on 07-06-2016
#endregion
namespace FriendshipConsole.Messages
{
	public class GreetFriend
	{
		public string FriendName { get; private set; }

		public GreetFriend( string friendName )
		{
			FriendName = friendName;
		}
	}
}