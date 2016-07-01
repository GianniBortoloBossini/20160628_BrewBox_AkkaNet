#region MailUp Header
// MailUp
// 
// Solution: Akka.IoT
// Project: Akka.IoT.App
// File: MqttDeviceTranslatorActor.cs
// 
// Last update: Gianni Bortolo Bossini on 18-06-2016
#endregion

using System;
using System.Configuration;
using Akka.Actor;
using Akka.IoT.MQTT.Infrastructure.Dtos;
using Akka.IoT.MQTT.Infrastructure.Topics;
using Akka.IoT.Shared.Dtos;
using Akka.IoT.Shared.Messages;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace Akka.IoT.App.Actors.MQTT
{
	public class DeviceMqttTranslatorActor : ReceiveActor
	{
		// Infrastructure/architecture
		private MqttClient _client = null;
		private readonly IActorRef _parent;
		private readonly IActorRef _self;
		private IActorRef _sender;
		private readonly string _deviceId;
		private readonly string _brokerAddress;

		// MQTT message topics
		private string _topicUnregister;
		private string _topicDisconnect;
		private string _topicStateChanged;
		private string _topicRegistered;
		private string _topicUnregistered;
		private string _topicSetDeviceState;
		private string _topicSetDeviceStateChanged;


		// Costructor
		public DeviceMqttTranslatorActor(string deviceId)
		{
			_parent = Context.Parent;
			_self = Self;

			_deviceId = deviceId;
			_brokerAddress = ConfigurationManager.AppSettings["BrokerAddress"];

			Become(Unregistered);
		}

		// Actor status
		private void Registered()
		{
			if (_client == null)
			{
				_topicUnregister = DeviceTopic.Unregister + "/" + _deviceId;
				_topicDisconnect = DeviceTopic.Disconnect + "/" + _deviceId;
				_topicStateChanged = DeviceTopic.DeviceStateChanged + "/" + _deviceId;
				_topicRegistered = DeviceTopic.Registered + "/" + _deviceId;
				_topicUnregistered = DeviceTopic.Unregistered + "/" + _deviceId;
				_topicSetDeviceState = DeviceTopic.SetDeviceState + "/" + _deviceId;
				_topicSetDeviceStateChanged = DeviceTopic.SetDeviceStateChanged + "/" + _deviceId;

				// create client instance 
				_client = new MqttClient(_brokerAddress);

				string clientId = ActorSystemReferences.ActorNames.DeviceActorNamePrefix + _deviceId;
				_client.Connect(clientId);
			}

			_client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;

			// subscribe to the topics with QoS 2 
			_client.Subscribe( new string[] {_topicUnregister}, new byte[] {MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE} );
			_client.Subscribe( new string[] {_topicDisconnect}, new byte[] {MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE} );
			_client.Subscribe( new string[] {_topicStateChanged}, new byte[] {MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE} );
			_client.Subscribe(new string[] { _topicSetDeviceStateChanged }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });

			_client.Publish( _topicRegistered, new byte[] {MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE} );

			Receive<UnregisterDevice>(msg => OnUnregisterDevice(msg));
			Receive<DisconnectDevice>(msg => OnDisconnectDevice(msg));
			Receive<SetDeviceStatus>( msg => OnSetDeviceStatus( msg ) );
		}

		private void Unregistered()
		{
			if (_client != null)
			{
				_client.Publish(_topicUnregistered, new byte[0]);

				_client.MqttMsgPublishReceived -= client_MqttMsgPublishReceived;

				_client.Unsubscribe(new string[] { _topicUnregister });
				_client.Unsubscribe(new string[] { _topicDisconnect });
				_client.Unsubscribe(new string[] { _topicStateChanged });
				_client.Unsubscribe(new string[] { _topicSetDeviceStateChanged });
			}

			Receive<RegisterDevice>(msg => OnRegisterDevice(msg));
			Receive<DisconnectDevice>(msg => OnDisconnectDevice(msg));
		}

		// Message event handler
		private void OnRegisterDevice( RegisterDevice msg )
		{
			_sender = Sender;

			Become(Registered);
		}

		private void OnSetDeviceStatus(SetDeviceStatus msg)
		{
			_sender = Sender;
			_client.Publish( DeviceTopic.SetDeviceState + "/" + _deviceId, ( new MqttDeviceStateDto( msg.Status ) ).Serialize() );
		}

		private void OnDisconnectDevice(DisconnectDevice msg)
		{
			_sender = Sender;
			_client.Disconnect();
		}

		private void OnUnregisterDevice(UnregisterDevice msg)
		{
			_sender = Sender;
			Become(Unregistered);
		}

		// Mqtt message event handler
		private void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
		{
			Console.WriteLine("DeviceActor: " + e.Topic);
			if (e.Topic == _topicUnregister)
			{
				_sender.Tell(new UnregisterDevice(), _self);
				_self.Tell(new UnregisterDevice(), _self);
			}

			if (e.Topic == _topicDisconnect)
			{
				_sender.Tell(new DisconnectDevice(), _self);
				_self.Tell(new DisconnectDevice(), _self);
			}

			if (e.Topic == _topicStateChanged)
			{
				MqttDeviceStateDto mqttDto = MqttDeviceStateDto.Deserialize( e.Message );
				_sender.Tell(new DeviceStateChanged(new DeviceStateDto(_deviceId, mqttDto.CurrentState, mqttDto.TimeStamp)), _self);
			}

			if ( e.Topic == _topicSetDeviceStateChanged )
			{
				MqttDeviceStateDto mqttDto = MqttDeviceStateDto.Deserialize(e.Message);
				_sender.Tell(new SetDeviceStatusReceived(new DeviceStateDto(_deviceId, mqttDto.CurrentState, mqttDto.TimeStamp)), _self);
			}
		}
	}
}