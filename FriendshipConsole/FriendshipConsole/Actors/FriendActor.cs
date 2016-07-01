#region MailUp Header
// MailUp
// 
// Solution: FriendshipConsole
// Project: FriendshipConsole
// File: PersonActor.cs
// 
// Last update: Gianni Bortolo Bossini on 05-06-2016
#endregion

using System.Threading.Tasks;
using Akka.Actor;
using Akka.Event;
using FriendshipConsole.Messages;

namespace FriendshipConsole.Actors
{
	public class FriendActor : ReceiveActor
	{
		public string Name { get; private set; }

		public FriendActor(string name)
		{
			Name = name;

			Receive<GreetFriendFrom>(msg => OnGreetFriendFrom(msg));
			Receive<BlameMe>(msg => OnBlameMe(msg));
			Receive<ForwardGreetingToWithOwner>(msg => OnForwardGreetingToWithOwner(msg));
		}

		private void OnForwardGreetingToWithOwner( ForwardGreetingToWithOwner msg )
		{
			Context.ActorSelection( "../friend_" + msg.TargetFriend ).Tell(new GreetFriendFrom(msg.OwnerName), Sender);
		}

		private void OnBlameMe( BlameMe msg )
		{
			Context.ActorSelection("/user/Application/io/Out").Tell(new BlameMeTo(friendToBlame: Name));
			
			Context.ActorSelection("..").Tell(new ByeByeOwner(Name));
			//Self.Tell(PoisonPill.Instance, Self);
		}

		private void OnGreetFriendFrom(GreetFriendFrom msg)
		{
			Context.ActorSelection("/user/Application/io/Out").Tell(new GreetFriendTo(to: Name, from: msg.From));
			
			Sender.Tell(new GreetFriendFrom(Name));
		}
	}
}