#region MailUp Header
// MailUp
// 
// Solution: FriendshipConsole
// Project: FriendshipConsole
// File: ReadUserInputResult.cs
// 
// Last update: Gianni Bortolo Bossini on 05-06-2016
#endregion
namespace FriendshipConsole.Messages.Document
{
	public class ReadUserInputResult
	{
		public string Result { get; private set; }

		public ReadUserInputResult(string result)
		{
			Result = result;
		}
	}
}