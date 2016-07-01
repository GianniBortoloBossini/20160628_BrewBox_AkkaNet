#region MailUp Header
// MailUp
// 
// Solution: Akka.IoT
// Project: Akka.IoT.MQTT.Infrastructure
// File: MqttDeviceInfoDto.cs
// 
// Last update: Gianni Bortolo Bossini on 17-06-2016
#endregion

using System;
using Akka.IoT.Shared.Enums;

namespace Akka.IoT.MQTT.Infrastructure.Dtos
{
	[Serializable]
	public class MqttDeviceInfoDto
	{
		public MqttDeviceInfoDto(string deviceId, string deviceName, int deviceGroup, DeviceTypes deviceType)
		{
			DeviceType = deviceType;
			DeviceGroup = deviceGroup;
			DeviceName = deviceName;
			DeviceId = deviceId;
		}

		public string DeviceId { get; private set; }
		public string DeviceName { get; private set; }
		public int DeviceGroup { get; private set; }
		public DeviceTypes DeviceType { get; private set; }
	}
}