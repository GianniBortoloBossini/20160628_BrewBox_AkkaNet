#region MailUp Header
// MailUp
// 
// Solution: FriendshipConsole
// Project: FriendshipConsole
// File: RemoveAllFriendsError.cs
// 
// Last update: Gianni Bortolo Bossini on 07-06-2016
#endregion
namespace FriendshipConsole.Messages
{
	public class RemoveAllFriendsError
	{
		public string Name { get; private set; }

		public RemoveAllFriendsError( string name )
		{
			Name = name;
		}
	}
}