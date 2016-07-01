#region MailUp Header
// MailUp
// 
// Solution: FriendshipConsole
// Project: FriendshipConsole
// File: ForwardGreetingActor.cs
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
	public class ForwardGreetingActor : StepActor
	{
		private readonly IActorRef _ownerActor;
		private string _forwarder;
		private string _target;

		public ForwardGreetingActor( IActorRef ownerActor) 
			: base( stepFilter: 5 )
		{
			_ownerActor = ownerActor;
		}

		protected override void CustomInitialize()
		{
			Receive<ForwardGreetingShowIntroCompleted>( msg => OnForwardGreetingShowIntroCompleted( msg ) );
			Receive<ReadUserInputResult>(msg => OnForwarderFrienNameReceived(msg));
			Receive<ForwardGreetingAskToCompleted>(msg => OnForwardGreetingAskToCompleted(msg));
		}

		private void OnForwardGreetingAskToCompleted(ForwardGreetingAskToCompleted msg)
		{
			Become(WaitForTheTargetOfTheGreeting);

			Context.ActorSelection("/user/Application/io/In").Tell(new ReadUserInput());
		}

		private void WaitForTheTargetOfTheGreeting()
		{
			Receive<ReadUserInputResult>(msg => OnTargetOfTheGreetingReceived(msg));
			Receive<ForwardGreetingError>( msg => OnForwardGreetingError( msg ) );
			Receive<ForwardGreetingSuccesfully>( msg => OnForwardGreetingSuccesfully( msg ) );
		}

		private void OnForwardGreetingSuccesfully( ForwardGreetingSuccesfully msg )
		{
			this.Initialize();
		}

		private void OnForwardGreetingError( ForwardGreetingError msg )
		{
			Context.ActorSelection("/user/Application/io/Out").Tell(msg);
		}

		private void OnTargetOfTheGreetingReceived( ReadUserInputResult msg )
		{
			_target = msg.Result;
			_ownerActor.Tell(new ForwardGreetingTo(forwarderFriend: _forwarder, targetFriend: _target));
		}

		private void OnForwarderFrienNameReceived( ReadUserInputResult msg )
		{
			_forwarder = msg.Result;
			Context.ActorSelection("/user/Application/io/Out").Tell(new ForwardGreetingAskTo(msg.Result));
		}

		private void OnForwardGreetingShowIntroCompleted( ForwardGreetingShowIntroCompleted msg )
		{
			Context.ActorSelection("/user/Application/io/In").Tell(new ReadUserInput());
		}

		public override void OnGoToStep( GoToStep msg )
		{
			Context.ActorSelection("/user/Application/io/Out").Tell(new ForwardGreetingShowIntro());
		}
	}
}