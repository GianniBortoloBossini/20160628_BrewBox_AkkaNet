#region MailUp Header
// MailUp
// 
// Solution: FriendshipConsole
// Project: FriendshipConsole
// File: StepActor.cs
// 
// Last update: Gianni Bortolo Bossini on 05-06-2016
#endregion

using Akka.Actor;
using FriendshipConsole.Messages.Command;

namespace FriendshipConsole.Actors.Steps
{
	public abstract class StepActor : ReceiveActor
	{
		private readonly int _stepFilter;

		protected StepActor(int stepFilter)
		{
			_stepFilter = stepFilter;

			Initialize();
		}

		protected void Initialize()
		{
			Become( () =>
					{
						Receive<GoToStep>( OnGoToStep, msg => msg.StepSelection == _stepFilter );
						CustomInitialize();
					} );
		}

		protected abstract void CustomInitialize();

		public abstract void OnGoToStep( GoToStep msg );
	}
}