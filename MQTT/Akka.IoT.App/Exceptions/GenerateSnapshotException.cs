#region MailUp Header
// MailUp
// 
// Solution: Akka.IoT
// Project: Akka.IoT.App
// File: GenerateSnapshotException.cs
// 
// Last update: Gianni Bortolo Bossini on 26-06-2016
#endregion

using System;

namespace Akka.IoT.App.Exceptions
{
	public class GenerateSnapshotException : Exception
	{
		public string Filename { get; private set; }

		public GenerateSnapshotException(string filename)
		{
			Filename = filename;
		}
	}
}