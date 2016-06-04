using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendshipConsole
{
	public class Friend
	{
		public Friend(string name)
		{
			Name = name;
		}

		public string Name { get; private set; }

		public static string GetNewFriendName()
		{
			Console.WriteLine();
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.Write("Insert here the name of your new friend: ");
			Console.ResetColor();
			return Console.ReadLine();
		}

		public void NiceToMeetYou(string friendName)
		{
			Console.WriteLine();
			Console.ForegroundColor = ConsoleColor.Magenta;
			Console.Write("Hi ");
			Console.ForegroundColor = ConsoleColor.Green;
			Console.Write(friendName);
			Console.ForegroundColor = ConsoleColor.Magenta;
			Console.Write( ", I'm " );
			Console.ForegroundColor = ConsoleColor.Green;
			Console.Write(Name);
			Console.ForegroundColor = ConsoleColor.Magenta;
			Console.WriteLine(". Nice to meet you!");
			
			Console.ResetColor();
			Console.WriteLine();
			Console.WriteLine("Press any key to continue...");
			Console.ReadLine();
			
			Console.Clear();
		}

		public void Greetings(string friendName)
		{
			Console.WriteLine();
			Console.ForegroundColor = ConsoleColor.Magenta;
			Console.Write("Hi ");
			Console.ForegroundColor = ConsoleColor.Green;
			Console.Write(Name);
			Console.ForegroundColor = ConsoleColor.Magenta;
			Console.WriteLine("!");
			Console.ForegroundColor = ConsoleColor.Magenta;
			Console.Write("Hi ");
			Console.ForegroundColor = ConsoleColor.Green;
			Console.Write(friendName);
			Console.ForegroundColor = ConsoleColor.Magenta;
			Console.WriteLine("!");
		}

		public static string GetExistingFriendName(string msg)
		{
			Console.WriteLine();
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.Write(msg);
			Console.ResetColor();
			return Console.ReadLine();
		}
	}
}
