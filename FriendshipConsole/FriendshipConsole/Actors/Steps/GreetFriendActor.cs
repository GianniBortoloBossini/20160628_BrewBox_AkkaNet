#region MailUp Header
// MailUp
// 
// Solution: FriendshipConsole
// Project: FriendshipConsole
// File: GreetFriendActor.cs
// 
// Last update: Gianni Bortolo Bossini on 07-06-2016
#endregion

using Akka.Actor;
using FriendshipConsole.Messages;
using FriendshipConsole.Messages.Command;
using FriendshipConsole.Messages.Document;

namespace FriendshipConsole.Actors.Steps
{
	public class GreetFriendActor : StepActor
	{
		private readonly IActorRef _ownerActor;

		public GreetFriendActor( IActorRef ownerActor ) 
			: base( stepFilter: 3 )
		{
			_ownerActor = ownerActor;
		}

		protected override void CustomInitialize()
		{
			Receive<GreetFriendShowIntroCompleted>(msg => OnGreetFriendShowIntroCompleted(msg));
			Receive<ReadUserInputResult>(msg => OnReadUserInputResult(msg));
			Receive<GreetFriendSuccesfully>(msg => OnGreetFriendSuccesfully(msg));
			Receive<GreetFriendError>(msg => OnGreetFriendError(msg));
		}

		public override void OnGoToStep( GoToStep msg )
		{
			Context.ActorSelection("/user/Application/io/Out").Tell(new GreetFriendShowIntro());
		}

		private void OnGreetFriendShowIntroCompleted(GreetFriendShowIntroCompleted msg)
		{
			Context.ActorSelection("/user/Application/io/In").Tell(new ReadUserInput());
		}

		private void OnReadUserInputResult(ReadUserInputResult msg)
		{
			_ownerActor.Tell(new GreetFriend(msg.Result));
		}

		private void OnGreetFriendError(GreetFriendError msg)
		{
			Context.ActorSelection("/user/Application/io/Out").Tell(msg);

			Become(Wait);
			Context.ActorSelection("/user/Application/io/In").Tell(new ReadUserInput());
		}

		private void OnGreetFriendSuccesfully(GreetFriendSuccesfully msg)
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