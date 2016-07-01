using System;
using System.Threading.Tasks;
using Akka.Actor;
using Akka.Dispatch.SysMsg;
using FriendshipConsole.Actors.IO;
using FriendshipConsole.Actors.Steps;
using FriendshipConsole.Messages;
using FriendshipConsole.Messages.Command;
using FriendshipConsole.Messages.Event;

namespace FriendshipConsole.Actors
{
	public class ApplicationActor : ReceiveActor
	{
		private readonly IActorRef _ioActor;
		private IActorRef _stepExitActor;
		private IActorRef _stepAddFriendActor;
		private IActorRef _stepRememberFriendsActor;
		private IActorRef _stepRemoveFriendActor;
		private IActorRef _stepGreetFriendActor;
		private IActorRef _stepGreetAllFriendsActor;
		private IActorRef _stepRemoveAllFriendsActor;
		private IActorRef _stepForwardGreetingActor;

		private bool _isFirstAccess;
		private string _ownerName;
		private IActorRef _ownerActor;
		


		private const string APPLICATION_NAME = "Friendship Console 2.0";

		public ApplicationActor()
		{
			_ioActor = Context.ActorOf(Props.Create<IOActor>(), "io");

			Become(Welcome);
		}

		private void Welcome()
		{
			Receive<ShowWelcomeCompleted>( msg => OnShowWelcomeMessageCompleted( msg ) );
			Receive<CreateOwnerCompleted>(msg => OnCreateOwnerCompleted(msg));

			_ioActor.Tell(new ShowWelcome(APPLICATION_NAME), Self);
		}

		private void Menu()
		{
			bool showTitle = !_isFirstAccess;
			_isFirstAccess = false;

			_ownerActor = Context.ActorOf(Props.Create<OwnerActor>(_ownerName), "owner");
			_stepExitActor = Context.ActorOf(Props.Create<ExitActor>(), "exit");
			_stepAddFriendActor = Context.ActorOf(Props.Create<AddFriendActor>(_ownerActor), "addfriend");
			_stepRememberFriendsActor = Context.ActorOf(Props.Create<RememberFriendsActor>(_ownerActor), "rememberfriends");
			_stepRemoveFriendActor = Context.ActorOf(Props.Create<RemoveFriendActor>(_ownerActor), "removefriend");
			_stepRemoveAllFriendsActor = Context.ActorOf(Props.Create<RemoveAllFriendsActor>(_ownerActor), "removeallfriends");
			_stepGreetFriendActor = Context.ActorOf(Props.Create<GreetFriendActor>(_ownerActor), "greetfriend");
			_stepGreetAllFriendsActor = Context.ActorOf(Props.Create<GreetAllFriendsActor>(_ownerActor), "greetallfriends");
			_stepForwardGreetingActor = Context.ActorOf(Props.Create<ForwardGreetingActor>(_ownerActor), "forwardgreeting");

			Receive<MenuCompleted>(msg => OnMenuCompleted(msg));
			Receive<ShowWrongChoiceCompleted>( msg => OnShowWrongChoiceCompleted( msg ) );

			Receive<ExitSelected>( msg => OnExitSelected( msg ) );
			Receive<AddFriendCompleted>(msg => OnAddNewFriendCompleted(msg));
			Receive<RememberFriendsCompleted>(msg => OnRememberCompleted(msg));
			Receive<RemoveFriendCompleted>(msg => OnRemoveFriendCompleted(msg));
			Receive<GreetFriendCompleted>(msg => OnGreetFriendCompleted(msg));
			Receive<RemoveAllFriendsCompleted>( msg => OnRemoveAllFriendsCompleted( msg ) );

			_ioActor.Tell(new ShowMenu(APPLICATION_NAME, ownerName: _ownerName, showTitle: showTitle), Self);
		}

		private void OnRemoveAllFriendsCompleted( RemoveAllFriendsCompleted msg )
		{
			_ioActor.Tell(new ShowMenu(APPLICATION_NAME, ownerName: _ownerName, showTitle: true), Self);
		}

		private void OnGreetFriendCompleted( GreetFriendCompleted msg )
		{
			_ioActor.Tell(new ShowMenu(APPLICATION_NAME, ownerName: _ownerName, showTitle: true), Self);
		}

		private void OnRemoveFriendCompleted( RemoveFriendCompleted msg )
		{
			_ioActor.Tell(new ShowMenu(APPLICATION_NAME, ownerName: _ownerName, showTitle: true), Self);
		}

		private void OnRememberCompleted( RememberFriendsCompleted msg )
		{
			_ioActor.Tell(new ShowMenu(APPLICATION_NAME, ownerName: _ownerName, showTitle: true), Self);
		}

		private void OnAddNewFriendCompleted( AddFriendCompleted msg )
		{
			_ioActor.Tell(new ShowMenu(APPLICATION_NAME, ownerName: _ownerName, showTitle: true), Self);
		}

		private void OnExitSelected( ExitSelected msg )
		{
			Context.System.Terminate();
		}

		private void OnShowWrongChoiceCompleted( ShowWrongChoiceCompleted msg )
		{
			_ioActor.Tell(new ShowMenu(APPLICATION_NAME, ownerName: _ownerName, showTitle: true), Self);
		}

		private void OnShowWelcomeMessageCompleted( ShowWelcomeCompleted msg )
		{
			_ioActor.Tell(new CreateOwner());
		}

		private void OnCreateOwnerCompleted(CreateOwnerCompleted msg)
		{
			_isFirstAccess = true;
			_ownerName = msg.OwnerName;

			Become(Menu);
		}

		private void OnMenuCompleted(MenuCompleted msg)
		{
			byte choiceIndex = 0;
			if ( byte.TryParse( msg.Selection, out choiceIndex ) &&
				( choiceIndex >= Choices.MinChoiceValue && choiceIndex <= Choices.MaxChoiceValue ) )
			{
				GoToStep command = new GoToStep( choiceIndex );

				_stepExitActor.Tell(command);
				_stepAddFriendActor.Tell(command);
				_stepRememberFriendsActor.Tell(command);
				_stepRemoveFriendActor.Tell(command);
				_stepGreetFriendActor.Tell(command);
				_stepGreetAllFriendsActor.Tell(command);
				_stepRemoveAllFriendsActor.Tell(command);
				_stepForwardGreetingActor.Tell(command);
			}
			else
			{
				_ioActor.Tell(new ShowWrongChoiceMessage(_ownerName));
			}
		}
		
	}
}
