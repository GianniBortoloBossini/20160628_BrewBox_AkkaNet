using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendshipConsole
{
	public class Choices
	{
		static Choices()
		{
			List = new Dictionary<int, string>();
			List.Add(1, "Add a new friend");
			List.Add(2, "Remove an existing friend");
			List.Add(3, "Greet a friend");
			List.Add(4, "Greet all your friends");
			List.Add(5, "Tell to a friend to greet another friend");
			List.Add(6, "Stop friendship killing all your friends");
			List.Add(7, "Remember all your friends");
			List.Add(8, "Exit kindly");

			MinChoiceValue = List.Keys.Min();
			MaxChoiceValue = List.Keys.Max();
		}

		public static int MaxChoiceValue { get; set; }

		public static int MinChoiceValue { get; set; }

		public static Dictionary<int, string> List { get; set; }
	}
}
