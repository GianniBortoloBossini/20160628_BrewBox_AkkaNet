#region MailUp Header
// MailUp
// 
// Solution: Akka.IoT
// Project: Akka.IoT.Infrastructure
// File: RegisterDevice.cs
// 
// Last update: Gianni Bortolo Bossini on 15-06-2016
#endregion

using Akka.IoT.Shared.Dtos;

namespace Akka.IoT.Shared.Messages
{
	public class RegisterDevice
	{
		public DeviceInfoDto DeviceDto { get; private set; }

		public RegisterDevice( DeviceInfoDto deviceDto )
		{
			DeviceDto = deviceDto;
		}
	}
}