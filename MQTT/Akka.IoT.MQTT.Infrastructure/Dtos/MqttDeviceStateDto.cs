#region MailUp Header
// MailUp
// 
// Solution: Akka.IoT
// Project: Akka.IoT.Infrastructure
// File: DeviceStateDto.cs
// 
// Last update: Gianni Bortolo Bossini on 15-06-2016
#endregion

using System;

namespace Akka.IoT.MQTT.Infrastructure.Dtos
{
	public class MqttDeviceStateDto
	{
		public bool CurrentState { get; private set; }
		public DateTime TimeStamp { get; private set; }

		public MqttDeviceStateDto(bool state)
		{
			CurrentState = state;
			TimeStamp = DateTime.UtcNow;
		}

		public byte[] Serialize()
		{
			return new byte[1] { Convert.ToByte(CurrentState) };
		}

		public static MqttDeviceStateDto Deserialize(byte[] mqttMessage)
		{
			return new MqttDeviceStateDto(Convert.ToBoolean(mqttMessage[0])); 
		}
	}
}