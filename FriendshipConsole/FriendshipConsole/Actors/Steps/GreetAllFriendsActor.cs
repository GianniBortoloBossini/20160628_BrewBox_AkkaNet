#region MailUp Header
// MailUp
// 
// Solution: FriendshipConsole
// Project: FriendshipConsole
// File: GreetFriends.cs
// 
// Last update: Gianni Bortolo Bossini on 07-06-2016
#endregion

using Akka.Actor;
using FriendshipConsole.Messages;
using FriendshipConsole.Messages.Command;
using FriendshipConsole.Messages.Document;

namespace FriendshipConsole.Actors.Steps
{
	public class GreetAllFriendsActor : StepActor
	{
		private readonly IActorRef _ownerActor;

		public GreetAllFriendsActor( IActorRef ownerActor ) : base( stepFilter: 4 )
		{
			_ownerActor = ownerActor;
		}

		protected override void CustomInitialize()
		{
			Receive<GreetAllFriendsError>( msg => OnGreetAllFriendsError( msg ) );
		}

		private void OnGreetAllFriendsError( GreetAllFriendsError msg )
		{
			Context.ActorSelection("/user/Application/io/Out").Tell(msg);

			Become(Wait);
			Context.ActorSelection("/user/Application/io/In").Tell(new ReadUserInput());
		}

		public override void OnGoToStep( GoToStep msg )
		{
			_ownerActor.Tell(new GreetAllFriends());
		}

		private void Wait()
		{
			Receive<ReadUserInputResult>(msg => WaitToContinue(msg));
		}

		private void WaitToContinue(ReadUserInputResult msg)
		{
			Context.ActorSelection("/user/Application/io/Out").Tell(new ClearOutput());
			Context.Parent.Tell(new RemoveFriendCompleted());

			base.Initialize();
		}
	}
}