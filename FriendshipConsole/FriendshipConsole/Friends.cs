using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendshipConsole
{
	public class Friends : List<Friend>
	{
		public Friend AddNewFriend(string name)
		{
			Friend friend = new Friend( name );

			this.Add(friend);

			return friend;
		}

		public int LeaveFriend( string existingFriendName )
		{
			int friendsRemoved = 0;
			if ( this.Any( f => f.Name == existingFriendName ) )
			{
				friendsRemoved = this.RemoveAll(f => f.Name == existingFriendName);
			}
			return friendsRemoved;
		}
	}
}
