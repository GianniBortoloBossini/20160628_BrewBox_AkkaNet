#region MailUp Header
// MailUp
// 
// Solution: Akka.IoT
// Project: Akka.IoT.App
// File: MqttPlantCoordinatorTranslatorActor.cs
// 
// Last update: Gianni Bortolo Bossini on 17-06-2016
#endregion

using System;
using System.Configuration;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Akka.Actor;
using Akka.IoT.Infrastructure.Messages;
using Akka.IoT.MQTT.Infrastructure.Dtos;
using Akka.IoT.MQTT.Infrastructure.Topics;
using Akka.IoT.Shared.Dtos;
using Akka.IoT.Shared.Messages;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace Akka.IoT.App.Actors.MQTT
{
	public class PlantCoordinatorMqttTranslatorActor : ReceiveActor
	{
		// Infrastructure/architecture
		private readonly IActorRef _self;
		private MqttClient _client;
		private IUntypedActorContext _context;
		private readonly string _brokerAddress;

		public PlantCoordinatorMqttTranslatorActor()
		{
			_self = Self;
			_context = Context;

			_brokerAddress = ConfigurationManager.AppSettings["BrokerAddress"];

			Become(Stopped);
		}

		// Actor status
		private void Started()
		{
			// create client instance 
			//MqttClient client = new MqttClient(IPAddress.Parse(MQTT_BROKER_ADDRESS));
			if (_client == null)
			{
				_client = new MqttClient(_brokerAddress);
				string clientId = Guid.NewGuid().ToString();
				_client.Connect(clientId);
			}

			// register to message received 
			_client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;

			Receive<StopPlantSupervisor>(msg => OnStopServerClient(msg));

			// subscribe to the topic "/home/temperature" with QoS 2 
			_client.Subscribe(new string[] { DeviceTopic.Register }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
		}

		private void Stopped()
		{
			if (_client != null)
			{
				// unregister to message received 
				_client.MqttMsgPublishReceived -= client_MqttMsgPublishReceived;
			}

			Receive<StartPlantCoordinator>(msg => OnStartServerClient(msg));
		}

		// Message handlers
		private void OnStartServerClient(StartPlantCoordinator msg)
		{
			Become(Started);
		}

		private void OnStopServerClient(StopPlantSupervisor msg)
		{
			Become(Stopped);
		}

		// Mqtt message event handler
		private void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
		{
			// handle message received 
			Console.WriteLine("MQTT msg received: " + e.Topic);

			switch (e.Topic)
			{
				case DeviceTopic.Register:
					{
						BinaryFormatter bf = new BinaryFormatter();
						MqttDeviceInfoDto dto = null;
						using (MemoryStream ms = new MemoryStream(e.Message))
						{
							dto = (MqttDeviceInfoDto)bf.Deserialize(ms);
						}

						_context.Parent.Tell(new RegisterDevice(new DeviceInfoDto(dto.DeviceId, dto.DeviceName, dto.DeviceGroup, dto.DeviceType)));
					} break;
			}
		} 
	}
}