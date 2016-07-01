#region MailUp Header
// MailUp
// 
// Solution: FriendshipConsole
// Project: FriendshipConsole
// File: InputActor.cs
// 
// Last update: Gianni Bortolo Bossini on 05-06-2016
#endregion

using System;
using Akka.Actor;
using FriendshipConsole.Messages.Command;
using FriendshipConsole.Messages.Document;

namespace FriendshipConsole.Actors.IO
{
	public class InputActor : ReceiveActor
	{
		public InputActor()
		{
			Receive<ReadUserInput>(msg => OnReadUserInput(msg));
		}

		private void OnReadUserInput( ReadUserInput msg )
		{
			Sender.Tell(new ReadUserInputResult(Console.ReadLine()));
		}
	}
}