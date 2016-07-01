#region MailUp Header
// MailUp
// 
// Solution: FriendshipConsole
// Project: FriendshipConsole
// File: RememberActor.cs
// 
// Last update: Gianni Bortolo Bossini on 06-06-2016
#endregion

using System.Threading.Tasks;
using Akka.Actor;
using FriendshipConsole.Messages;
using FriendshipConsole.Messages.Command;
using FriendshipConsole.Messages.Document;

namespace FriendshipConsole.Actors.Steps
{
	public class RememberFriendsActor : StepActor
	{
		private readonly IActorRef _ownerActor;
		private string _ownerName;

		public RememberFriendsActor(IActorRef ownerActor) 
			: base( stepFilter: 7 )
		{
			_ownerActor = ownerActor;
		}

		protected override void CustomInitialize()
		{
			Receive<RememberFriendsShowIntroCompleted>( msg => OnRememberFriendsShowIntroCompleted( msg ) );
			Receive<RememberFriendsSuccesfully>( msg => OnRememberFriendsSuccesfully( msg ) );
			Receive<RememberFriendsError>(msg => OnRememberFriendsError(msg));
		}

		private void OnRememberFriendsError( RememberFriendsError msg )
		{
			Context.ActorSelection("/user/Application/io/Out").Tell(msg);

			Become(Wait);
			Context.ActorSelection("/user/Application/io/In").Tell(new ReadUserInput());
		}

		private void OnRememberFriendsSuccesfully( RememberFriendsSuccesfully msg )
		{
			Context.ActorSelection("/user/Application/io/Out").Tell(msg);

			Become(Wait);
			Context.ActorSelection("/user/Application/io/In").Tell(new ReadUserInput());
		}

		private void Wait()
		{
			Receive<ReadUserInputResult>(msg => WaitToContinue(msg));
		}

		private void WaitToContinue( ReadUserInputResult msg )
		{
			Context.ActorSelection("/user/Application/io/Out").Tell(new ClearOutput());
			Context.Parent.Tell(new RememberFriendsCompleted());

			base.Initialize();
		}

		private void OnRememberFriendsShowIntroCompleted( RememberFriendsShowIntroCompleted msg )
		{
			_ownerActor.Tell(new RememberFriends());
		}

		public override void OnGoToStep( GoToStep msg )
		{
			_ownerName = _ownerActor.Ask<string>( new WhatIsYourName() ).Result;
			Context.ActorSelection("/user/Application/io/Out").Tell(new RememberFriendsShowIntro(_ownerName));
		}
	}
}