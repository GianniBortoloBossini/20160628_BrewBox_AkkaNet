#region MailUp Header
// MailUp
// 
// Solution: Akka.IoT
// Project: Akka.IoT.Infrastructure
// File: DeviceStateDto.cs
// 
// Last update: Gianni Bortolo Bossini on 15-06-2016
#endregion

using System;

namespace Akka.IoT.Shared.Dtos
{
	public class DeviceStateDto
	{
		public string DeviceId { get; private set; }
		public bool CurrentState { get; private set; }
		public DateTime TimeStamp { get; private set; }

		public static DeviceStateDto Empty
		{
			get
			{
				return new DeviceStateDto("", false, DateTime.MinValue);
			}
		}

		public DeviceStateDto(string deviceId, bool state, DateTime timestamp)
		{
			DeviceId = deviceId;
			CurrentState = state;
			TimeStamp = timestamp;
		}
	}
}