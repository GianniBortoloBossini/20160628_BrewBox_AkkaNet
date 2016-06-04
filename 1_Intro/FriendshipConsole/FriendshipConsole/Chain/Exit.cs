using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendshipConsole.Chain
{
	public class Exit : Handler
	{
		public override void HandleRequest(int choice)
		{
			if (choice == 8)
			{
				throw new ExitException();
			}
			else if (Successor != null)
			{
				Successor.HandleRequest(choice);
			}
		}
	}
}
