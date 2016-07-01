#region MailUp Header
// MailUp
// 
// Solution: FriendshipConsole
// Project: FriendshipConsole
// File: ShowTypeOwnerNameAfter.cs
// 
// Last update: Gianni Bortolo Bossini on 05-06-2016
#endregion

using System.Threading.Tasks;

namespace FriendshipConsole.Messages.Command
{
	public class CreateOwnerShowMessageAfter
	{
		public string OwnerName { get; private set; }

		public CreateOwnerShowMessageAfter( string ownerName )
		{
			OwnerName = ownerName;
		}
	}
}