#region MailUp Header
// MailUp
// 
// Solution: FriendshipConsole
// Project: FriendshipConsole
// File: GreetFriendError.cs
// 
// Last update: Gianni Bortolo Bossini on 07-06-2016
#endregion
namespace FriendshipConsole.Messages
{
	public class GreetFriendError
	{
		public string FriendName { get; private set; }
		public string OwnerName { get; private set; }

		public GreetFriendError( string friendName, string ownerName )
		{
			FriendName = friendName;
			OwnerName = ownerName;
		}
	}
}