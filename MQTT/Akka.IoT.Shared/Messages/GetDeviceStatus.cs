#region MailUp Header
// MailUp
// 
// Solution: Akka.IoT
// Project: Akka.IoT.Shared
// File: Class1.cs
// 
// Last update: Gianni Bortolo Bossini on 17-06-2016
#endregion
namespace Akka.IoT.Shared.Messages
{
	public class GetDeviceStatus
	{
		public string DeviceId { get; private set; }

		public GetDeviceStatus()
		{
		}

		public GetDeviceStatus(string deviceId)
		{
			DeviceId = deviceId;
		}
	}
}