#region MailUp Header
// MailUp
// 
// Solution: Akka.IoT
// Project: Akka.IoT.Shared
// File: SetDeviceStatusReceived.cs
// 
// Last update: Gianni Bortolo Bossini on 19-06-2016
#endregion

using Akka.IoT.Shared.Dtos;

namespace Akka.IoT.Shared.Messages
{
	public class SetDeviceStatusReceived
	{
		public DeviceStateDto DeviceStateDto { get; private set; }
		public bool UnexistingDevice { get; private set; }

		public SetDeviceStatusReceived(bool unexistingDevice)
		{
			DeviceStateDto = null;
			UnexistingDevice = unexistingDevice;
		}

		public SetDeviceStatusReceived( DeviceStateDto deviceStateDto )
		{
			DeviceStateDto = deviceStateDto;
			UnexistingDevice = false;
		}
	}
}