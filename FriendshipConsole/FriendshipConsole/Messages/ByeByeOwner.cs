﻿#region MailUp Header
// MailUp
// 
// Solution: FriendshipConsole
// Project: FriendshipConsole
// File: ByeByeOwner.cs
// 
// Last update: Gianni Bortolo Bossini on 07-06-2016
#endregion
namespace FriendshipConsole.Messages
{
	public class ByeByeOwner
	{
		public string Name { get; private set; }

		public ByeByeOwner( string name )
		{
			Name = name;
		}
	}
}