#region MailUp Header
// MailUp
// 
// Solution: FriendshipConsole
// Project: FriendshipConsole
// File: AddNewFriend.cs
// 
// Last update: Gianni Bortolo Bossini on 05-06-2016
#endregion
namespace FriendshipConsole.Messages
{
	public class AddFriend
	{
		public string Name { get; private set; }

		public AddFriend(string name)
		{
			Name = name;
		}
	}
}