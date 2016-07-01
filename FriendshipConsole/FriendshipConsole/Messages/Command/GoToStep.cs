#region MailUp Header
// MailUp
// 
// Solution: FriendshipConsole
// Project: FriendshipConsole
// File: GoToStep.cs
// 
// Last update: Gianni Bortolo Bossini on 05-06-2016
#endregion

using FriendshipConsole.Actors;
using FriendshipConsole.Actors.Steps;

namespace FriendshipConsole.Messages.Command
{
	public class GoToStep
	{
		public int StepSelection { get; private set; }

		public GoToStep(int stepSelection)
		{
			StepSelection = stepSelection;
		}
	}
}