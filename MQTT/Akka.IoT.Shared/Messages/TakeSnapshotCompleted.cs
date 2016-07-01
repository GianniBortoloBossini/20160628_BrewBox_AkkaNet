#region MailUp Header
// MailUp
// 
// Solution: Akka.IoT
// Project: Akka.IoT.App
// File: TakeSnapshotCompleted.cs
// 
// Last update: Gianni Bortolo Bossini on 21-06-2016
#endregion

using System;

namespace Akka.IoT.Shared.Messags
{
	public class TakeSnapshotCompleted
	{
		public Guid Id { get; private set; }
		public string FileName { get; private set; }

		public TakeSnapshotCompleted( Guid id, string fileName )
		{
			Id = id;
			FileName = fileName;
		}
	}
}