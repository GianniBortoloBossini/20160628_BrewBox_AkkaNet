using Akka.Actor;
using FriendshipConsole.Actors.IO;
using FriendshipConsole.Messages.Command;
using FriendshipConsole.Messages.Document;
using FriendshipConsole.Messages.Event;

namespace FriendshipConsole.Actors
{
	public class IOActor : ReceiveActor
	{
		private readonly IActorRef _inputActor;
		private readonly IActorRef _outputActor;

		public IOActor()
		{
			_inputActor = Context.ActorOf( Props.Create<InputActor>(), "In");
			_outputActor = Context.ActorOf(Props.Create<OutputActor>(), "Out");

			Become(Welcome);
		}

		private void OnShowWelcome( ShowWelcome msg )
		{
			_outputActor.Forward(msg);
		}

		private void Welcome()
		{
			Receive<ShowWelcome>(msg => OnShowWelcome(msg));
			Receive<CreateOwner>(msg => OnCreateOwner(msg));
			Receive<CreateOwnerShowMessageBeforeCompleted>(evt => OnCreateOwnerShowMessageBeforeCompleted(evt));
			Receive<ReadUserInputResult>(doc => OnCreateOwnerResult(doc));
			Receive<ShowMenu>(msg => OnShowMenu(msg));
		}

		private void OnShowMenu(ShowMenu msg)
		{
			Become(Menu);

			_outputActor.Tell(msg);
		}

		private void Menu()
		{
			Receive<ShowMenu>(msg => OnShowMenu(msg));
			Receive<ShowMenuShowMessageBeforeCompleted>(evt => OnShowMenuShowMessageBeforeCompleted(evt));
			Receive<ReadUserInputResult>(doc => OnMenuResult(doc));
			Receive<ShowWrongChoiceMessage>( msg => OnShowWrongChoiceMessage( msg ) );
		}

		private void OnShowWrongChoiceMessage( ShowWrongChoiceMessage msg )
		{
			_outputActor.Forward(msg);
		}

		private void OnShowMenuShowMessageBeforeCompleted(ShowMenuShowMessageBeforeCompleted msg)
		{
			_inputActor.Tell(new ReadUserInput());
		}

		private void OnCreateOwner(CreateOwner msg)
		{
			_outputActor.Tell(new CreateOwnerShowMessageBefore());
		}

		private void OnCreateOwnerShowMessageBeforeCompleted( CreateOwnerShowMessageBeforeCompleted msg )
		{
			_inputActor.Tell(new ReadUserInput());
		}

		private void OnCreateOwnerResult(ReadUserInputResult doc)
		{
			_outputActor.Tell(new CreateOwnerShowMessageAfter(doc.Result));
			Context.Parent.Tell(new CreateOwnerCompleted(doc.Result));
		}

		private void OnMenuResult(ReadUserInputResult doc)
		{
			Context.Parent.Tell(new MenuCompleted(doc.Result));
		}

	}
}
