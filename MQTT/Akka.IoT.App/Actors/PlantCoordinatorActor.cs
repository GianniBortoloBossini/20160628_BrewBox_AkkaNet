#region MailUp Header
// MailUp
// 
// Solution: Akka.IoT
// Project: Akka.IoT.App
// File: PlantCoordinatorActor.cs
// 
// Last update: Gianni Bortolo Bossini on 14-06-2016
#endregion

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Akka.Actor;
using Akka.IoT.App.Actors.MQTT;
using Akka.IoT.Infrastructure.Messages;
using Akka.IoT.Shared.Dtos;
using Akka.IoT.Shared.Messages;

namespace Akka.IoT.App.Actors
{
	public class PlantCoordinatorActor : ReceiveActor
	{
		// Architecture/infrastructure
		private readonly IActorRef _mqttPlantCoordinatorTranslatorActor;
		private readonly IActorRef _webCamActor;
		private bool _isInitialized = false;

		// Costructor
		public PlantCoordinatorActor()
		{
			_mqttPlantCoordinatorTranslatorActor =
				Context.ActorOf(Props.Create<PlantCoordinatorMqttTranslatorActor>(), ActorSystemReferences.ActorNames.PlantCoordinatorMqttTranslatorActorName);

			_webCamActor = Context.ActorOf(Props.Create<SnapshotCoordinatorActor>(), ActorSystemReferences.ActorNames.SnapshotCoordinatorActorName);

			Become(Stopped);
		}

		// Actor status
		private void Started()
		{
			_mqttPlantCoordinatorTranslatorActor.Tell(new StartPlantCoordinator());

			Receive<RegisterDevice>(msg => OnRegisterDevice(msg));
			Receive<GetAllDevicesStatus>(msg => OnGetAllDevicesStatus(msg));
			Receive<GetDeviceStatus>(msg => OnGetDeviceStatus(msg));
			Receive<SetDeviceStatus>(msg => OnSetDeviceStatus(msg));
			Receive<StopPlantSupervisor>(msg => OnStopServerClient(msg));
			Receive<TakeSnapshot>( msg => OnTakeSnapshot( msg ) );
		}

		private void Stopped()
		{
			if (_isInitialized)
				_mqttPlantCoordinatorTranslatorActor.Tell(new StopPlantSupervisor());

			Receive<StartPlantCoordinator>( msg => OnStartServerClient( msg ) );

			_isInitialized = true;
		}

		// Message handlers
		private void OnSetDeviceStatus(SetDeviceStatus msg)
		{
			string uniqueDeviceActorName = ActorSystemReferences.ActorNames.DeviceActorNamePrefix + msg.DeviceId;

			IActorRef deviceActorRef = Context.Child(uniqueDeviceActorName);
			if ( deviceActorRef.IsNobody() )
			{
				Sender.Tell(new SetDeviceStatusReceived(unexistingDevice: true), Self);
			}
			else
			{
				deviceActorRef.Forward(msg); 
			}
		}
		
		private void OnRegisterDevice(RegisterDevice msg)
		{
			DeviceInfoDto dto = msg.DeviceDto;
			string uniqueDeviceActorName = ActorSystemReferences.ActorNames.DeviceActorNamePrefix + dto.DeviceId;
			
			IActorRef deviceActorRef = Context.Child(uniqueDeviceActorName);
			if (deviceActorRef.IsNobody())
				deviceActorRef = Context.ActorOf(Props.Create<DeviceActor>(), uniqueDeviceActorName);
			deviceActorRef.Tell(msg);
		}

		private void OnGetAllDevicesStatus( GetAllDevicesStatus msg )
		{
			List<Task<GetDeviceStatusReceived>> tasks = new List<Task<GetDeviceStatusReceived>>();
			IEnumerable<IActorRef> childrenActors = Context.GetChildren();
			childrenActors = childrenActors.Where( ca => ca.Path.Name.Contains( ActorSystemReferences.ActorNames.DeviceActorNamePrefix ) );

			if ( !childrenActors.Any())
			{
				Sender.Tell( new GetAllDevicesStatusReceived( new List<DeviceStateDto>() ) );
			}
			else
			{
				foreach ( IActorRef actorRef in childrenActors )
				{
					tasks.Add(actorRef.Ask<GetDeviceStatusReceived>(new GetDeviceStatus()));
				}
				Task.WhenAll( tasks ).ContinueWith( act =>
													{
														List<DeviceStateDto> result = new List<DeviceStateDto>();
														foreach ( GetDeviceStatusReceived statusReceived in act.Result )
														{
															result.Add(new DeviceStateDto(statusReceived.DeviceState.DeviceId, statusReceived.DeviceState.CurrentState, statusReceived.DeviceState.TimeStamp));
														}
														return new GetAllDevicesStatusReceived(result);
													}).PipeTo(Sender);
			}
		}

		private void OnGetDeviceStatus(GetDeviceStatus msg)
		{
			IActorRef deviceActor = Context.Child( ActorSystemReferences.ActorNames.DeviceActorNamePrefix + msg.DeviceId );
			if ( deviceActor.IsNobody() )
				Sender.Tell( new GetDeviceStatusReceived( DeviceStateDto.Empty ) );
			else
			{
				GetDeviceStatusReceived result = deviceActor.Ask<GetDeviceStatusReceived>( msg ).Result;
				Sender.Tell( new GetDeviceStatusReceived( new DeviceStateDto(result.DeviceState.DeviceId, result.DeviceState.CurrentState, result.DeviceState.TimeStamp)));
			}

		}

		private void OnTakeSnapshot(TakeSnapshot msg)
		{
			_webCamActor.Forward(msg);
		}

		private void OnStartServerClient(StartPlantCoordinator msg)
		{
			Become(Started);
		}

		private void OnStopServerClient( StopPlantSupervisor msg )
		{
			Become( Stopped );
		}
	}
}