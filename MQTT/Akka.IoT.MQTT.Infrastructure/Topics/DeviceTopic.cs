#region MailUp Header
// MailUp
// 
// Solution: Akka.IoT
// Project: Akka.IoT.MQTT.Infrastructure
// File: Device.cs
// 
// Last update: Gianni Bortolo Bossini on 14-06-2016
#endregion
namespace Akka.IoT.MQTT.Infrastructure.Topics
{
	public class DeviceTopic
	{
		public const string Register = @"/device/registerme";
		public const string Registered = @"/device/registered";
		public const string Unregister = @"/device/unregisterme";
		public const string Unregistered = @"/device/unregistered";
		public const string Disconnect = @"/device/disconnect";
		public const string DeviceStateChanged = @"/device/statechanged";
		public const string SetDeviceState = @"/device/setthisstate";
		public const string SetDeviceStateChanged = @"/device/setstatechanged";
	}
}