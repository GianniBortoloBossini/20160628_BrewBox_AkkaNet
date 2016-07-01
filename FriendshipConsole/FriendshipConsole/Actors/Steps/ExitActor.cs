#region MailUp Header
// MailUp
// 
// Solution: FriendshipConsole
// Project: FriendshipConsole
// File: ExitActor.cs
// 
// Last update: Gianni Bortolo Bossini on 05-06-2016
#endregion

using System.Threading.Tasks;
using Akka.Actor;
using FriendshipConsole.Messages.Command;
using FriendshipConsole.Messages.Document;
using FriendshipConsole.Messages.Event;

namespace FriendshipConsole.Actors.Steps
{
	public class ExitActor : StepActor
	{
		public ExitActor() 
			: base( stepFilter: 8)
		{
			
		}

		protected override void CustomInitialize()
		{
			
		}

		private void Wait()
		{
			Receive<ReadUserInputResult>(doc => WaitToExit(doc));

			Context.ActorSelection("/user/Application/io/In").Tell(new ReadUserInput());
		}

		private void WaitToExit( ReadUserInputResult doc )
		{
			Context.ActorSelection("/user/Application/io/Out").Tell(new ClearOutput());
			Context.Parent.Tell(new ExitSelected());

			base.Initialize();
		}

		public override void OnGoToStep( GoToStep msg )
		{
			Context.ActorSelection("/user/Application/io/Out").Tell(new ExitSelected());

			Become(Wait);
		}


	}
}