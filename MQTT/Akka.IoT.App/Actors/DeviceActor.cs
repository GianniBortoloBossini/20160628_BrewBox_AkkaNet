#region MailUp Header
// MailUp
// 
// Solution: Akka.IoT
// Project: Akka.IoT.App
// File: DeviceActor.cs
// 
// Last update: Gianni Bortolo Bossini on 15-06-2016
#endregion

using System;
using Akka.Actor;
using Akka.IoT.App.Actors.MQTT;
using Akka.IoT.Shared.Dtos;
using Akka.IoT.Shared.Messages;

namespace Akka.IoT.App.Actors
{
	public class DeviceActor : ReceiveActor
	{
		// Architecture/infrastructure
		private IActorRef _mqttDeviceTranslatorActor;

		// Device state
		private DeviceInfoDto _deviceDto = null;
		private DeviceStateDto _deviceStateDto = null;
		private bool _deviceState;
		private DateTime _deviceStateEventTimeStamp;
		private DateTime _deviceStateProcessedTimeStamp;

		// Costructor
		public DeviceActor()
		{
			Become(Unregistered);
		}
		
		// Actor status
		private void Registered()
		{
			string uniqueDeviceActorName = ActorSystemReferences.ActorNames.DeviceMqttTranslatorActorNamePrefix + _deviceDto.DeviceId;
			IActorRef deviceActor = Context.Child(uniqueDeviceActorName);
			if(deviceActor.IsNobody())
				_mqttDeviceTranslatorActor = Context.ActorOf(Props.Create<DeviceMqttTranslatorActor>(_deviceDto.DeviceId), ActorSystemReferences.ActorNames.DeviceMqttTranslatorActorNamePrefix + _deviceDto.DeviceId);
			
			_mqttDeviceTranslatorActor.Tell(new RegisterDevice(_deviceDto));

			Receive<UnregisterDevice>( msg => OnUnregisterDevice( msg ) );
			Receive<DisconnectDevice>( msg => OnDisconnectDevice( msg ) );
			Receive<DeviceStateChanged>( msg => OnDeviceStateChanged( msg ) );
			Receive<GetDeviceStatus>( msg => OnGetDeviceStatus( msg ) );
			Receive<SetDeviceStatus>( msg => OnSetDeviceStatus( msg ) );
			Receive<SetDeviceStateChanged>(msg => OnSetDeviceStateChanged(msg));
		}

		private void Unregistered()
		{
			Receive<RegisterDevice>(msg => OnRegisterDevice(msg));
			Receive<DisconnectDevice>(msg => OnDisconnectDevice(msg));
		}

		// Message handler
		private void OnSetDeviceStatus(SetDeviceStatus msg)
		{
			string uniqueDeviceActorName = ActorSystemReferences.ActorNames.DeviceMqttTranslatorActorNamePrefix + _deviceDto.DeviceId;
			IActorRef deviceActor = Context.Child(uniqueDeviceActorName);
			if (deviceActor.IsNobody())
				_mqttDeviceTranslatorActor = Context.ActorOf(Props.Create<DeviceMqttTranslatorActor>(_deviceDto.DeviceId), ActorSystemReferences.ActorNames.DeviceMqttTranslatorActorNamePrefix + _deviceDto.DeviceId);

			_mqttDeviceTranslatorActor.Forward(msg);
		}

		private void OnSetDeviceStateChanged(SetDeviceStateChanged msg)
		{
			Context.Parent.Tell(new SetDeviceStatusReceived(msg.DeviceStateDto), Self);
		}
		
		private void OnGetDeviceStatus( GetDeviceStatus msg )
		{
			Sender.Tell(new GetDeviceStatusReceived(new DeviceStateDto(_deviceDto.DeviceId, _deviceState, _deviceStateEventTimeStamp)), Self);
		}

		private void OnDeviceStateChanged( DeviceStateChanged msg )
		{
			_deviceStateDto = msg.DeviceStateDto;
			_deviceState = msg.DeviceStateDto.CurrentState;
			_deviceStateEventTimeStamp = msg.DeviceStateDto.TimeStamp;
			_deviceStateProcessedTimeStamp = DateTime.UtcNow;
		}

		private void OnUnregisterDevice(UnregisterDevice msg)
		{
			Become(Unregistered);
		}

		private void OnRegisterDevice( RegisterDevice msg )
		{
			_deviceDto = msg.DeviceDto;

			Become(Registered);
		}

		private void OnDisconnectDevice(DisconnectDevice msg)
		{
			Self.Tell(PoisonPill.Instance);
		}
	}
}