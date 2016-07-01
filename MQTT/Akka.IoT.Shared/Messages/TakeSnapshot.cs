#region MailUp Header
// MailUp
// 
// Solution: Akka.IoT
// Project: Akka.IoT.Shared
// File: TakeSnapshot.cs
// 
// Last update: Gianni Bortolo Bossini on 21-06-2016
#endregion

using System;

namespace Akka.IoT.Shared.Messages
{
	public class TakeSnapshot
	{
		public Uri Uri { get; private set; }
		public Guid Id { get; private set; }

		public TakeSnapshot(Uri uri)
		{
			Uri = uri;
			Id = Guid.Empty;
		}

		public TakeSnapshot( Uri uri, Guid id )
		{
			Uri = uri;
			Id = id;
		}
	}
}