#region MailUp Header
// MailUp
// 
// Solution: FriendshipConsole
// Project: FriendshipConsole
// File: OwnerActor.cs
// 
// Last update: Gianni Bortolo Bossini on 05-06-2016
#endregion

using System.Collections.Generic;
using System.Linq;
using Akka.Actor;
using FriendshipConsole.Actors.Steps;
using FriendshipConsole.Messages;
using FriendshipConsole.Messages.Command;

namespace FriendshipConsole.Actors
{
	public class OwnerActor : ReceiveActor
	{
		private readonly Dictionary<string, IActorRef> _friends;
		public string Name { get; private set; }

		public OwnerActor(string name)
		{
			Name = name;

			_friends = new Dictionary<string, IActorRef>();

			Receive<AddFriend>( msg => OnAddNewFriend( msg ) );
			Receive<WhatIsYourName>(msg => Sender.Tell(Name));
			Receive<RememberFriends>( msg => OnRememberFriends( msg ) );
			Receive<RemoveFriend>(msg => OnRemoveFriend(msg));
			Receive<GreetFriend>(msg => OnGreetFriend(msg));
			Receive<GreetFriendFrom>(msg => OnGreetFriendFrom(msg));
			Receive<GreetAllFriends>(msg => OnGreetAllFriends(msg));
			Receive<RemoveAllFriends>( msg => OnRemoveAllFriends( msg ) );
			Receive<ByeByeOwner>(msg => OnByeByeOwner(msg));
			Receive<ForwardGreetingTo>( msg => OnForwardGreetingTo( msg ) );
		}

		private void OnForwardGreetingTo( ForwardGreetingTo msg )
		{
			if ( _friends.ContainsKey( msg.ForwarderFriend ) && _friends.ContainsKey( msg.TargetFriend ) )
			{
				_friends[msg.ForwarderFriend].Tell(new ForwardGreetingToWithOwner(Name, msg.ForwarderFriend, msg.TargetFriend));
				Sender.Tell(new ForwardGreetingSuccesfully());
			}
			else
			{
				Sender.Tell(new ForwardGreetingError(Name, msg.ForwarderFriend, msg.TargetFriend));
			}
		}

		private void OnByeByeOwner( ByeByeOwner msg )
		{
			Context.System.EventStream.Unsubscribe(_friends[msg.Name], typeof(GreetFriendFrom));
			Context.System.EventStream.Unsubscribe(_friends[msg.Name], typeof(BlameMe));
			Context.Stop(_friends[msg.Name]);
			_friends.Remove(msg.Name);

			if(!_friends.Any())
				Context.ActorSelection("/user/Application/removeallfriends").Tell( new RemoveAllFriendsSuccesfully(Name), Self );
		}

		private void OnRemoveAllFriends( RemoveAllFriends msg )
		{
			if ( _friends.Any() )
			{
				Context.System.EventStream.Publish(new BlameMe());
			}
			else
				Sender.Tell(new RemoveAllFriendsError(Name));
		}

		private void OnGreetFriendFrom( GreetFriendFrom msg )
		{
			Context.ActorSelection("/user/Application/io/Out").Tell(new GreetFriendTo(Name, msg.From));

			Context.ActorSelection("/user/Application/greetfriend").Tell(new GreetFriendSuccesfully(Name));
		}

		private void OnGreetAllFriends( GreetAllFriends msg )
		{
			if(_friends.Any())
				Context.System.EventStream.Publish(new GreetFriendFrom(Name));
			else
				Sender.Tell(new GreetAllFriendsError(Name));
		}

		private void OnGreetFriend( GreetFriend msg )
		{
			if ( _friends.ContainsKey( msg.FriendName ) )
			{
				_friends[msg.FriendName].Tell(new GreetFriendFrom(from: Name));
			}
			else
			{
				Sender.Tell(new GreetFriendError(msg.FriendName, Name));
			}
		}

		private void OnRemoveFriend( RemoveFriend msg )
		{
			if ( _friends.ContainsKey( msg.FriendName ) )
			{
				Context.System.EventStream.Unsubscribe(_friends[msg.FriendName], typeof(GreetFriendFrom));
				Context.System.EventStream.Unsubscribe(_friends[msg.FriendName], typeof(BlameMe));
				Context.Stop( _friends[msg.FriendName] );
				_friends.Remove( msg.FriendName );

				Sender.Tell(new RemoveFriendSuccesfully(msg.FriendName, Name));
			}
			else
			{
				Sender.Tell(new RemoveFriendError(msg.FriendName, Name));
			}
		}

		private void OnRememberFriends( RememberFriends msg )
		{
			if(_friends.Any())
				Sender.Tell(new RememberFriendsSuccesfully(Name, _friends.Keys));
			else
				Sender.Tell(new RememberFriendsError(Name));
		}

		private void OnAddNewFriend( AddFriend msg )
		{
			if ( _friends.ContainsKey( msg.Name ) )
			{
				Sender.Tell(new AddFriendError(friendName: msg.Name, ownerName: Name));
			}
			else
			{
				IActorRef friendActor = Context.ActorOf( Props.Create<FriendActor>( msg.Name ), "friend_" + msg.Name );
				Context.System.EventStream.Subscribe(friendActor, typeof(GreetFriendFrom));
				Context.System.EventStream.Subscribe(friendActor, typeof(BlameMe));
				_friends.Add(msg.Name, friendActor);
				Sender.Tell(new AddFriendSuccesfully(friendName: msg.Name, ownerName: Name));
			}
		}
	}
}