#region MailUp Header
// MailUp
// 
// Solution: FriendshipConsole
// Project: FriendshipConsole
// File: RemoveAllFriendsActor.cs
// 
// Last update: Gianni Bortolo Bossini on 07-06-2016
#endregion

using System.Threading.Tasks;
using Akka.Actor;
using FriendshipConsole.Messages;
using FriendshipConsole.Messages.Command;
using FriendshipConsole.Messages.Document;

namespace FriendshipConsole.Actors.Steps
{
	public class RemoveAllFriendsActor : StepActor
	{
		private readonly IActorRef _ownerActor;

		public RemoveAllFriendsActor( IActorRef ownerActor ) : base( stepFilter: 6 )
		{
			_ownerActor = ownerActor;
		}

		protected override void CustomInitialize()
		{
			Receive<RemoveAllFriendsShowIntroCompleted>( msg => OnRemoveAllFriendsShowIntroCompleted( msg ) );
			Receive<RemoveAllFriendsSuccesfully>( msg => OnRemoveAllFriendsSuccesfully( msg ) );
			Receive<RemoveAllFriendsError>(msg => OnRemoveAllFriendsError(msg));
		}

		private void OnRemoveAllFriendsError( RemoveAllFriendsError msg )
		{
			Context.ActorSelection("/user/Application/io/Out").Tell(msg);

			Become(Wait);
			Context.ActorSelection("/user/Application/io/In").Tell(new ReadUserInput());
		}

		private void OnRemoveAllFriendsSuccesfully( RemoveAllFriendsSuccesfully msg )
		{
			Context.ActorSelection("/user/Application/io/Out").Tell(msg);

			Become(Wait);
			Context.ActorSelection("/user/Application/io/In").Tell(new ReadUserInput());
		}

		private void OnRemoveAllFriendsShowIntroCompleted( RemoveAllFriendsShowIntroCompleted msg )
		{
			_ownerActor.Tell(new RemoveAllFriends());
		}

		private void Wait()
		{
			Receive<ReadUserInputResult>(msg => WaitToContinue(msg));
		}

		private void WaitToContinue(ReadUserInputResult msg)
		{
			Context.ActorSelection("/user/Application/io/Out").Tell(new ClearOutput());
			Context.Parent.Tell(new RemoveAllFriendsCompleted());

			base.Initialize();
		}

		public override void OnGoToStep( GoToStep msg )
		{
			Context.ActorSelection("/user/Application/io/Out").Tell(new RemoveAllFriendsShowIntro());
		}
	}
}