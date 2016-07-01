#region MailUp Header
// MailUp
// 
// Solution: Akka.IoT
// Project: Akka.IoT.Shared
// File: SetDeviceStatus.cs
// 
// Last update: Gianni Bortolo Bossini on 19-06-2016
#endregion

using System.Runtime.CompilerServices;

namespace Akka.IoT.Shared.Messages
{
	public class SetDeviceStatus
	{
		public string DeviceId { get; private set; }
		public bool Status { get; private set; }

		public SetDeviceStatus(string deviceId, bool status)
		{
			DeviceId = deviceId;
			Status = status;
		}
	}
}