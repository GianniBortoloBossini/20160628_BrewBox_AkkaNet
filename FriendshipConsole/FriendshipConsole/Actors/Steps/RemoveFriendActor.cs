#region MailUp Header
// MailUp
// 
// Solution: FriendshipConsole
// Project: FriendshipConsole
// File: RemoveFriendActor.cs
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
	public class RemoveFriendActor : StepActor
	{
		private readonly IActorRef _ownerActor;

		public RemoveFriendActor( IActorRef ownerActor ) 
			: base( stepFilter: 2 )
		{
			_ownerActor = ownerActor;
		}

		protected override void CustomInitialize()
		{
			Receive<RemoveFriendShowIntroCompleted>(msg => OnRemoveFriendShowIntroCompleted(msg));
			Receive<ReadUserInputResult>(msg => OnReadUserInputResult(msg));
			Receive<RemoveFriendSuccesfully>(msg => OnRemoveFriendSuccesfully(msg));
			Receive<RemoveFriendError>(msg => OnRemoveFriendError(msg));
		}

		public override void OnGoToStep(GoToStep msg)
		{
			Context.ActorSelection("/user/Application/io/Out").Tell(new RemoveFriendShowIntro());
		}

		private void OnReadUserInputResult(ReadUserInputResult msg)
		{
			_ownerActor.Tell(new RemoveFriend(msg.Result));
		}

		private void OnRemoveFriendShowIntroCompleted( RemoveFriendShowIntroCompleted msg )
		{
			Context.ActorSelection("/user/Application/io/In").Tell(new ReadUserInput());
		}

		private void OnRemoveFriendSuccesfully(RemoveFriendSuccesfully msg)
		{
			Context.ActorSelection("/user/Application/io/Out").Tell(msg);

			Become(Wait);
			Context.ActorSelection("/user/Application/io/In").Tell(new ReadUserInput());
		}

		private void OnRemoveFriendError( RemoveFriendError msg )
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
			Context.Parent.Tell(new RemoveFriendCompleted());

			base.Initialize();
		}
	}
}