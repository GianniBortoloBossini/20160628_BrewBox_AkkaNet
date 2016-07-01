#region MailUp Header
// MailUp
// 
// Solution: Akka.IoT
// Project: Akka.IoT.Broker
// File: MqttClientActor.cs
// 
// Last update: Gianni Bortolo Bossini on 11-06-2016
#endregion

using System;
using Akka.Actor;
using Akka.IoT.MQTT.Infrastructure.Messages;
using Akka.IoT.MQTT.Infrastructure.Topics;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace Akka.IoT.MQTT.Translator.Actors
{
	public class MqttTranslatorActor : ReceiveActor
	{
		private MqttClient _client = null;

		public MqttTranslatorActor()
		{
			Become(Stopped);
		}

		private void Stopped()
		{
			if ( _client != null )
			{
				// register to message received 
				_client.MqttMsgPublishReceived -= client_MqttMsgPublishReceived;

				// subscribe to the topic "/home/temperature" with QoS 2 
				_client.Unsubscribe( new string[] {"/home/temperature"} );
			}

			Receive<StartTranslator>( msg => OnStartServerClient( msg ) );
		}

		private void Started()
		{
			// create client instance 
			//MqttClient client = new MqttClient(IPAddress.Parse(MQTT_BROKER_ADDRESS));
			if ( _client == null )
			{
				_client = new MqttClient("localhost");
				string clientId = Guid.NewGuid().ToString();
				_client.Connect(clientId); 
			}

			// register to message received 
			_client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;

			// subscribe to the topic "/home/temperature" with QoS 2 
			_client.Subscribe(new string[] { DeviceTopic.Register }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
			_client.Subscribe(new string[] { DeviceTopic.Unregister }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });

			Receive<StopTranslator>(msg => OnStopServerClient(msg));
		}

		private void OnStartServerClient(StartTranslator msg)
		{
			Become(Started);
		}

		private void OnStopServerClient(StopTranslator msg)
		{
			Become(Stopped);
		}

		private void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
		{
			switch ( e.Topic )
			{
				case DeviceTopic.Register:
					_client.Publish( DeviceTopic.Registered, new byte[0] );
					break;
				case DeviceTopic.Unregister:
					_client.Publish(DeviceTopic.Unregistered, new byte[0]);
					break;
			}

			
				

			// handle message received 
			Console.WriteLine("MQTT msg received: " + e.Topic);
		} 
	}
}