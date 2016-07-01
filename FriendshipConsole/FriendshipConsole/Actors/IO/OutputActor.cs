#region MailUp Header
// MailUp
// 
// Solution: FriendshipConsole
// Project: FriendshipConsole
// File: OutputActor.cs
// 
// Last update: Gianni Bortolo Bossini on 05-06-2016
#endregion

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Akka.Actor;
using Akka.IO;
using FriendshipConsole.Actors.Steps;
using FriendshipConsole.Messages;
using FriendshipConsole.Messages.Command;
using FriendshipConsole.Messages.Event;

namespace FriendshipConsole.Actors.IO
{
	public class OutputActor : ReceiveActor
	{
		public OutputActor()
		{
			Receive<ShowWelcome>(msg => OnShowWelcome(msg));
			Receive<ShowMenu>(msg => OnShowMenu(msg));
			Receive<CreateOwnerShowMessageBefore>(msg => OnShowTypeOwnerNameBefore(msg));
			Receive<CreateOwnerShowMessageAfter>( msg => OnShowTypeOwnerNameAfter( msg ) );
			Receive<ShowWrongChoiceMessage>( msg => OnShowWrongChoiceMessage( msg ) );
			Receive<ExitSelected>( msg => OnExitSelected( msg ) );
			
			Receive<AddFriendShowIntro>( msg => OnAddFriendShowIntro( msg ) );
			Receive<AddFriendSuccesfully>( msg => OnAddFriendSuccesfully( msg ) );
			Receive<AddFriendError>(msg => OnAddFriendError(msg));
			
			Receive<RememberFriendsShowIntro>( msg => OnRememberFriendsShowIntro( msg ) );
			Receive<RememberFriendsError>( msg => OnRememberFriendsError( msg ) );
			Receive<RememberFriendsSuccesfully>(msg => OnRememberFriendsSuccesfully(msg));
			
			Receive<RemoveFriendShowIntro>( msg => OnRemoveFriendShowIntro( msg ) );
			Receive<RemoveFriendError>( msg => OnRemoveFriendError( msg ) );
			Receive<RemoveFriendSuccesfully>(msg => OnRemoveFriendSuccesfully(msg));

			Receive<GreetFriendShowIntro>( msg => OnGreetFriendShowIntro( msg ) );
			Receive<GreetFriendError>(msg => OnGreetFriendError(msg));
			Receive<GreetFriendSuccesfully>(msg => OnGreetFriendSuccesfully(msg));
			Receive<GreetFriendTo>(msg => OnGreetFriendTo(msg));
			Receive<GreetAllFriendsError>( msg => OnGreetAllFriendsError( msg ) );

			Receive<RemoveAllFriendsShowIntro>( msg => OnRemoveAllFriendsShowIntro( msg ) );
			Receive<RemoveAllFriendsSuccesfully>( msg => OnRemoveAllFriendsSuccesfully( msg ) );
			Receive<RemoveAllFriendsError>( msg => OnRemoveAllFriendsError( msg ) );
			Receive<BlameMeTo>(msg => OnBlameMeTo(msg));

			Receive<ForwardGreetingShowIntro>( msg => OnForwardGreetingShowIntro( msg ) );
			Receive<ForwardGreetingAskTo>( msg => OnForwardGreetingAskTo( msg ) );
			Receive<ForwardGreetingError>(msg => OnForwardGreetingError(msg));
			
			Receive<ClearOutput>(msg => OnClearOutput(msg));
		}

		private void OnForwardGreetingError( ForwardGreetingError msg )
		{
			Console.WriteLine();
			Console.ForegroundColor = ConsoleColor.Red;
			Console.Write("Ops! You haven't got any friend named ");
			Console.ForegroundColor = ConsoleColor.Green;
			Console.Write(msg.ForwarderFriend);
			Console.ForegroundColor = ConsoleColor.Red;
			Console.Write(" or ");
			Console.ForegroundColor = ConsoleColor.Green;
			Console.Write(msg.TargetFriend);
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine(".");
			Console.WriteLine();
			Console.Write("Please, pay attention ");
			Console.ForegroundColor = ConsoleColor.Green;
			Console.Write(msg.OwnerName);
			Console.WriteLine("!");
			Console.ResetColor();
			Console.WriteLine();
			Console.WriteLine("Press any key to continue...");
		}

		private void OnForwardGreetingAskTo( ForwardGreetingAskTo msg )
		{
			Console.WriteLine();
			Console.ForegroundColor = ConsoleColor.Magenta;
			Console.Write("Please ");
			Console.ForegroundColor = ConsoleColor.Green;
			Console.Write(msg.ForwarderFriendName);
			Console.ForegroundColor = ConsoleColor.Magenta;
			Console.Write(", forward my greetings to: ");
			Console.ResetColor();

			Sender.Tell(new ForwardGreetingAskToCompleted());
		}

		private void OnForwardGreetingShowIntro( ForwardGreetingShowIntro msg )
		{
			Console.WriteLine();
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.Write("Insert the name of the friend that should forward your greetings: ");
			Console.ResetColor();

			Sender.Tell(new ForwardGreetingShowIntroCompleted());
		}

		private void OnRemoveAllFriendsError( RemoveAllFriendsError msg )
		{
			Console.ForegroundColor = ConsoleColor.Red;
			Console.Write("Ops! You are ALONE!!!");
			Console.WriteLine();
			Console.Write("Please, pay attention ");
			Console.ForegroundColor = ConsoleColor.Green;
			Console.Write(msg.Name);
			Console.WriteLine("!");
			Console.ResetColor();
			Console.WriteLine();
			Console.WriteLine("Press any key to continue...");
		}

		private void OnRemoveAllFriendsSuccesfully( RemoveAllFriendsSuccesfully msg )
		{
			Console.ForegroundColor = ConsoleColor.Magenta;
			Console.WriteLine("Now you are ALONE!!! Are your happy?!?!");
			Console.ResetColor();
			Console.WriteLine();
			Console.WriteLine("Press any key to continue...");
		}

		private void OnBlameMeTo( BlameMeTo msg )
		{
			Console.ForegroundColor = ConsoleColor.Magenta;
			Console.Write("F*@k you, ");
			Console.ForegroundColor = ConsoleColor.Green;
			Console.Write(msg.FriendToBlame);
			Console.ForegroundColor = ConsoleColor.Magenta;
			Console.WriteLine("!!!");
		}

		private void OnRemoveAllFriendsShowIntro( RemoveAllFriendsShowIntro msg )
		{
			Console.WriteLine();
			Console.ForegroundColor = ConsoleColor.Magenta;
			Console.WriteLine("These were my friends! ");
			Console.WriteLine();

			Sender.Tell(new RemoveAllFriendsShowIntroCompleted());
		}

		private void OnGreetAllFriendsError( GreetAllFriendsError msg )
		{
			Console.WriteLine();
			Console.ForegroundColor = ConsoleColor.Red;
			Console.Write("Ops! You haven't got friends!");
			Console.WriteLine();
			Console.Write("Please, pay attention ");
			Console.ForegroundColor = ConsoleColor.Green;
			Console.Write(msg.OwnerName);
			Console.WriteLine("!");
			Console.ResetColor();
			Console.WriteLine();
			Console.WriteLine("Press any key to continue...");
		}

		private void OnGreetFriendTo(GreetFriendTo msg)
		{
			Console.WriteLine();
			Console.ForegroundColor = ConsoleColor.Magenta;
			Console.Write("Hi ");
			Console.ForegroundColor = ConsoleColor.Green;
			Console.Write(msg.To);
			Console.ForegroundColor = ConsoleColor.Magenta;
			Console.Write(" from ");
			Console.ForegroundColor = ConsoleColor.Green;
			Console.Write(msg.From);
			Console.WriteLine("!");
		}

		private void OnGreetFriendSuccesfully( GreetFriendSuccesfully msg )
		{
			Console.ResetColor();
			Console.WriteLine();
			Console.WriteLine("Press any key to continue...");
		}

		private void OnGreetFriendError( GreetFriendError msg )
		{
			Console.WriteLine();
			Console.ForegroundColor = ConsoleColor.Red;
			Console.Write("Ops! You haven't got any friend named ");
			Console.ForegroundColor = ConsoleColor.Green;
			Console.Write(msg.FriendName);
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine(".");
			Console.WriteLine();
			Console.Write("Please, pay attention ");
			Console.ForegroundColor = ConsoleColor.Green;
			Console.Write(msg.OwnerName);
			Console.WriteLine("!");
			Console.ResetColor();
			Console.WriteLine();
			Console.WriteLine("Press any key to continue...");
		}

		private void OnGreetFriendShowIntro( GreetFriendShowIntro msg )
		{
			Console.WriteLine();
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.Write("Insert here the name of the friend you want to greet: ");
			Console.ResetColor();

			Sender.Tell(new GreetFriendShowIntroCompleted());
		}

		private void OnRemoveFriendError(RemoveFriendError msg)		
		{
			Console.WriteLine();
			Console.ForegroundColor = ConsoleColor.Red;
			Console.Write("Ops! You haven't got any friend named ");
			Console.ForegroundColor = ConsoleColor.Green;
			Console.Write(msg.FriendName);
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine(".");
			Console.WriteLine();
			Console.Write("Please, pay attention ");
			Console.ForegroundColor = ConsoleColor.Green;
			Console.Write(msg.OwnerName);
			Console.WriteLine("!");
			Console.ResetColor();
			Console.WriteLine();
			Console.WriteLine("Press any key to continue...");
		}

		private void OnRemoveFriendSuccesfully( RemoveFriendSuccesfully msg )		
		{
			Console.WriteLine();
			Console.ForegroundColor = ConsoleColor.Magenta;
			Console.Write("I'm sorry ");
			Console.ForegroundColor = ConsoleColor.Green;
			Console.Write(msg.FriendName);
			Console.ForegroundColor = ConsoleColor.Magenta;
			Console.WriteLine(", but we cannot be friend!");

			Console.ResetColor();
			Console.WriteLine();
			Console.WriteLine("Press any key to continue...");
		}

		private void OnRemoveFriendShowIntro( RemoveFriendShowIntro msg )
		{
			Console.WriteLine();
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.Write("Insert here the name of the friend you want to leave: ");
			Console.ResetColor();

			Sender.Tell(new RemoveFriendShowIntroCompleted());
		}


		private void OnRememberFriendsSuccesfully( RememberFriendsSuccesfully msg )
		{
			foreach (string friendName in msg.Friends)
			{
				Console.WriteLine(friendName);
			}
			Console.ResetColor();
			Console.WriteLine();
			Console.WriteLine("Press any key to continue...");
		}

		private void OnRememberFriendsError( RememberFriendsError msg )
		{
			Console.WriteLine();
			Console.ForegroundColor = ConsoleColor.Magenta;
			Console.Write("Hey ");
			Console.ForegroundColor = ConsoleColor.Green;
			Console.Write(msg.OwnerName);
			Console.ForegroundColor = ConsoleColor.Magenta;
			Console.WriteLine(", you are alone, ALONE!!!");
			Console.ResetColor();
			Console.WriteLine();
			Console.WriteLine("Press any key to continue...");
		}

		private void OnRememberFriendsShowIntro( RememberFriendsShowIntro msg )
		{
			Console.WriteLine();
			Console.ForegroundColor = ConsoleColor.Magenta;
			Console.Write("Hey ");
			Console.ForegroundColor = ConsoleColor.Green;
			Console.Write(msg.OwnerName);
			Console.ForegroundColor = ConsoleColor.Magenta;
			Console.WriteLine(", do you remember your friends?");
			Console.WriteLine();

			Sender.Tell(new RememberFriendsShowIntroCompleted());
		}


		private void OnClearOutput( ClearOutput msg )
		{
			Console.Clear();
		}

		
		private void OnAddFriendError( AddFriendError msg )
		{
			Console.WriteLine();
			Console.ForegroundColor = ConsoleColor.Red;
			Console.Write("Ops! You have already got a friend named ");
			Console.ForegroundColor = ConsoleColor.Green;
			Console.Write(msg.FriendName);
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine("!");
			Console.WriteLine();
			Console.Write("Please, pay attention ");
			Console.ForegroundColor = ConsoleColor.Green;
			Console.Write(msg.OwnerName);
			Console.WriteLine("!");
			Console.ResetColor();
			Console.WriteLine();
			Console.WriteLine("Press any key to continue...");
		}

		private void OnAddFriendSuccesfully( AddFriendSuccesfully msg )
		{
			Console.WriteLine();
			Console.ForegroundColor = ConsoleColor.Magenta;
			Console.Write("Hi ");
			Console.ForegroundColor = ConsoleColor.Green;
			Console.Write(msg.OwnerName);
			Console.ForegroundColor = ConsoleColor.Magenta;
			Console.Write(", I'm ");
			Console.ForegroundColor = ConsoleColor.Green;
			Console.Write(msg.FriendName);
			Console.ForegroundColor = ConsoleColor.Magenta;
			Console.WriteLine(". Nice to meet you!");

			Console.ResetColor();
			Console.WriteLine();
			Console.WriteLine("Press any key to continue...");
		}

		private void OnAddFriendShowIntro( AddFriendShowIntro msg )
		{
			Console.WriteLine();
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.Write("Insert here the name of your new friend: ");
			Console.ResetColor();

			Sender.Tell(new AddFriendShowIntroCompleted());
		}

		
		private void OnExitSelected( ExitSelected msg )
		{
			Console.WriteLine();
			Console.WriteLine("Bye bye!");
		}

		private void OnShowWrongChoiceMessage( ShowWrongChoiceMessage msg )
		{
			Console.WriteLine();
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine("Ops! You made a wrong choice.");
			Console.Write("Please, pay attention ");
			Console.ForegroundColor = ConsoleColor.Green;
			Console.Write(msg.OwnerName);
			Console.WriteLine("!");
			Console.ResetColor();
			Console.WriteLine();
			Console.WriteLine("Press any key to continue...");
			Console.ReadLine();
			Console.Clear();

			Context.Sender.Tell(new ShowWrongChoiceCompleted());
		}

		private void OnShowMenu( ShowMenu msg )
		{
			if ( msg.ShowTitle )
			{
				Console.ForegroundColor = ConsoleColor.Cyan;
				Console.WriteLine(msg.ApplicationName);
				Console.ResetColor();
				Console.WriteLine();
			}

			Console.ForegroundColor = ConsoleColor.Green;
			Console.Write(msg.OwnerName);
			Console.ResetColor();
			Console.WriteLine(", please tell me what you want to do:");
			foreach (KeyValuePair<int, string> choiceItem in Choices.List)
			{
				Console.WriteLine("{0} - {1}", choiceItem.Key, choiceItem.Value);
			}

			Console.WriteLine();
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.Write("Make your choice: ");
			Console.ResetColor();

			Context.Parent.Tell(new ShowMenuShowMessageBeforeCompleted());
		}

		private void OnShowWelcome( ShowWelcome msg )
		{
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.WriteLine("Welcome to {0}!", msg.ApplicationName);
			Console.ResetColor();
			Console.WriteLine();

			Context.Sender.Tell(new ShowWelcomeCompleted());
		}

		private void OnShowTypeOwnerNameBefore(CreateOwnerShowMessageBefore msg)
		{
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.Write("Please insert your name or nickname: ");
			Console.ResetColor();

			Context.Parent.Tell(new CreateOwnerShowMessageBeforeCompleted());
		}

		private void OnShowTypeOwnerNameAfter(CreateOwnerShowMessageAfter msg)
		{
			Console.WriteLine();

			Console.Write("Nice to meet you ");
			Console.ForegroundColor = ConsoleColor.Green;
			Console.Write(msg.OwnerName);
			Console.ResetColor();
			Console.WriteLine(".");
			Console.WriteLine();

			Context.Parent.Tell(new CreateOwnerShowMessageAfterCompleted());
		}
	}
}