#region MailUp Header
// MailUp
// 
// Solution: Akka.IoT
// Project: Akka.IoT.Shared
// File: SetDeviceStateChanged.cs
// 
// Last update: Gianni Bortolo Bossini on 20-06-2016
#endregion

using Akka.IoT.Shared.Dtos;

namespace Akka.IoT.Shared.Messages
{
	public class SetDeviceStateChanged
	{
		public SetDeviceStateChanged(DeviceStateDto deviceStateDto)
		{
			DeviceStateDto = deviceStateDto;
		}

		public DeviceStateDto DeviceStateDto { get; private set; }
	}
}