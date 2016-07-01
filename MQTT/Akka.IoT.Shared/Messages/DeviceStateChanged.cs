#region MailUp Header
// MailUp
// 
// Solution: Akka.IoT
// Project: Akka.IoT.Infrastructure
// File: Class1.cs
// 
// Last update: Gianni Bortolo Bossini on 15-06-2016
#endregion

using Akka.IoT.Shared.Dtos;

namespace Akka.IoT.Shared.Messages
{
	public class DeviceStateChanged
	{
		public DeviceStateChanged(DeviceStateDto deviceStateDto)
		{
			DeviceStateDto = deviceStateDto;
		}

		public DeviceStateDto DeviceStateDto { get; private set; }
	}
}