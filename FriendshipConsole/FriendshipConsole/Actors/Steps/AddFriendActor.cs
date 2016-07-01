#region MailUp Header
// MailUp
// 
// Solution: FriendshipConsole
// Project: FriendshipConsole
// File: AddFriendActor.cs
// 
// Last update: Gianni Bortolo Bossini on 05-06-2016
#endregion

using System.Threading.Tasks;
using Akka.Actor;
using FriendshipConsole.Messages;
using FriendshipConsole.Messages.Command;
using FriendshipConsole.Messages.Document;
using FriendshipConsole.Messages.Event;

namespace FriendshipConsole.Actors.Steps
{
	public class AddFriendActor : StepActor
	{
		private readonly IActorRef _ownerActor;

		public AddFriendActor(IActorRef ownerActor) 
			: base( stepFilter: 1 )
		{
			_ownerActor = ownerActor;
		}

		protected override void CustomInitialize()
		{
			Receive<AddFriendShowIntroCompleted>(msg => OnStepAddFriendShowMessageBefore(msg));
			Receive<ReadUserInputResult>(msg => OnReadUserInputResult(msg));
			Receive<AddFriendSuccesfully>(msg => OnAddNewFriendSuccesfully(msg));
			Receive<AddFriendError>(msg => OnAddNewFriendError(msg));
		}

		private void Wait()
		{
			Receive<ReadUserInputResult>(msg => WaitToContinue(msg));
		}

		private void OnStepAddFriendShowMessageBefore(AddFriendShowIntroCompleted msg)
		{
			Context.ActorSelection("/user/Application/io/In").Tell(new ReadUserInput());
		}

		public override void OnGoToStep( GoToStep msg )
		{
			Context.ActorSelection("/user/Application/io/Out").Tell(new AddFriendShowIntro());
		}

		private void OnReadUserInputResult(ReadUserInputResult doc)
		{
			_ownerActor.Tell(new AddFriend(name: doc.Result));
		}

		private void OnAddNewFriendError( AddFriendError msg )
		{
			Context.ActorSelection("/user/Application/io/Out").Tell(msg);

			Become(Wait);
			Context.ActorSelection("/user/Application/io/In").Tell(new ReadUserInput());
		}

		private void OnAddNewFriendSuccesfully( AddFriendSuccesfully msg )
		{
			Context.ActorSelection( "/user/Application/io/Out" ).Tell( msg );

			Become(Wait);
			Context.ActorSelection("/user/Application/io/In").Tell(new ReadUserInput());
		}

		private void WaitToContinue(ReadUserInputResult msg)
		{
			Context.ActorSelection("/user/Application/io/Out").Tell(new ClearOutput());
			Context.Parent.Tell(new AddFriendCompleted());

			base.Initialize();
		}
	}
}