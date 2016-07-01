#region MailUp Header
// MailUp
// 
// Solution: Akka.IoT
// Project: Akka.IoT.App
// File: WebCamActor.cs
// 
// Last update: Gianni Bortolo Bossini on 21-06-2016
#endregion

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Akka.Actor;
using Akka.IoT.App.Exceptions;
using Akka.IoT.Shared.Messages;
using Akka.IoT.Shared.Messags;

namespace Akka.IoT.App.Actors
{
	public class SnapshotCoordinatorActor : ReceiveActor
	{
		private class SnapshotActorRef
		{
			public SnapshotActorRef(Uri uri)
			{
				Uri = uri;
				Id = Guid.NewGuid(); 
				IsBusy = false;
				ActorReference = Context.ActorOf(Props.Create<SnapshotActor>(Uri), ActorSystemReferences.ActorNames.SnapshotActorNamePrefix + Id);
			}

			public IActorRef ActorReference { get; private set; }
			public Uri Uri { get; private set; }
			public bool IsBusy { get; private set; }
			public Guid Id { get; private set; }

			public void SetAsBusy()
			{
				IsBusy = true;
			}

			public void SetAsAvailable()
			{
				IsBusy = false;
			}
		}

		private readonly List<SnapshotActorRef> _snapshots;

		public SnapshotCoordinatorActor()
		{
			_snapshots = new List<SnapshotActorRef>();

			Receive<TakeSnapshot>( msg => OnTakeSnapshot( msg ) );
			Receive<TakeSnapshotCompleted>(msg => OnTakeSnapshotCompleted(msg));
		}

		private void OnTakeSnapshot( TakeSnapshot msg )
		{
			IActorRef snapshotActorRef;
			SnapshotActorRef snapshotActorRefItem;
			if ( _snapshots.Any( wc => !wc.IsBusy ) )
			{
				snapshotActorRefItem = _snapshots.First( wc => !wc.IsBusy );
			}
			else
			{
				snapshotActorRefItem = new SnapshotActorRef( msg.Uri );
				_snapshots.Add( snapshotActorRefItem );
			}
			snapshotActorRef = snapshotActorRefItem.ActorReference;

			snapshotActorRefItem.SetAsBusy();

			snapshotActorRef.Forward(new TakeSnapshot(msg.Uri, snapshotActorRefItem.Id));
		}

		private void OnTakeSnapshotCompleted(TakeSnapshotCompleted msg)
		{
			_snapshots.Find( wc => wc.Id == msg.Id ).SetAsAvailable();
		}

		protected override SupervisorStrategy SupervisorStrategy()
		{
			return new OneForOneStrategy(decider: new LocalOnlyDecider( exc =>
																		{
																			if ( exc is GenerateSnapshotException )
																			{
																				string sourceFileName = Path.Combine( Directory.GetCurrentDirectory(), "Images", "error.jpg" );
																				File.Copy(sourceFileName, ((GenerateSnapshotException)exc).Filename);
																				
																				return Directive.Resume;
																			}
																			else 
																				return Directive.Restart;
																		}));
		}
	}
}