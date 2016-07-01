#region MailUp Header
// MailUp
// 
// Solution: Akka.IoT
// Project: Akka.IoT.Infrastructure
// File: MqttMessageReceived.cs
// 
// Last update: Gianni Bortolo Bossini on 15-06-2016
#endregion

using Akka.IoT.Shared.Dtos;

namespace Akka.IoT.MQTT.Infrastructure.Messages
{
	public class MqttReceiveMessageReceived
	{
		 public DeviceInfoDto DeviceDto { get; private set; }

		 public MqttReceiveMessageReceived(DeviceInfoDto deviceDto)
		{
			DeviceDto = deviceDto;
		}
	}
}