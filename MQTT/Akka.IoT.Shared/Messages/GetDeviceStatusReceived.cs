#region MailUp Header
// MailUp
// 
// Solution: Akka.IoT
// Project: Akka.IoT.Shared
// File: GetDeviceStatusReceived.cs
// 
// Last update: Gianni Bortolo Bossini on 16-06-2016
#endregion

using Akka.IoT.Shared.Dtos;

namespace Akka.IoT.Shared.Messages
{
	public class GetDeviceStatusReceived
	{
		public DeviceStateDto DeviceState { get; private set; }

		public GetDeviceStatusReceived(DeviceStateDto deviceState)
		{
			DeviceState = deviceState;
		}
	}
}