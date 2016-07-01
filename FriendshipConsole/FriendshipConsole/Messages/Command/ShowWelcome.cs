namespace FriendshipConsole.Messages.Command
{
	public class ShowWelcome
	{
		public string ApplicationName { get; private set; }

		public ShowWelcome(string applicationName)
		{
			ApplicationName = applicationName;
		}
	}
}
