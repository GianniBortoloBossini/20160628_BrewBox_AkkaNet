using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FriendshipConsole.Actors.Steps
{
	public class ForwardGreetingError
	{
		public string OwnerName { get; private set; }
		public string ForwarderFriend { get; private set; }
		public string TargetFriend { get; private set; }

		public ForwardGreetingError( string ownerName, string forwarderFriend, string targetFriend )
		{
			OwnerName = ownerName;
			ForwarderFriend = forwarderFriend;
			TargetFriend = targetFriend;
		}
	}
}
