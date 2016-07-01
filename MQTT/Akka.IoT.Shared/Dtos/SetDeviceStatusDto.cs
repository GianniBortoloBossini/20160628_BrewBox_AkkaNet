#region MailUp Header
// MailUp
// 
// Solution: Akka.IoT
// Project: Akka.IoT.Shared
// File: SetDeviceStatusDto.cs
// 
// Last update: Gianni Bortolo Bossini on 19-06-2016
#endregion
namespace Akka.IoT.Shared.Dtos
{
	public class SetDeviceStatusDto
	{
		public string DeviceId { get; private set; }
		public bool Status { get; private set; }

		public SetDeviceStatusDto(string deviceId, bool status)
		{
			DeviceId = deviceId;
			Status = status;
		}
	}
}