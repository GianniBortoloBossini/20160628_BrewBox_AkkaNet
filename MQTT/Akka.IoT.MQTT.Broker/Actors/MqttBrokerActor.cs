using Akka.Actor;
using Akka.IoT.MQTT.Infrastructure.Messages;
using uPLibrary.Networking.M2Mqtt;

namespace Akka.IoT.Broker.Actors
{
	public class MqttBrokerActor : ReceiveActor
	{
		private MqttBroker _broker = null;

		public MqttBrokerActor()
		{
			Become( Stopped );
		}

		private void Stopped()
		{
			if ( _broker != null )
			{
				_broker.Stop();
			}
			
			Receive<StartMQTTBroker>(msg => OnStartBroker());
		}

		private void Started()
		{
			if(_broker == null)
				_broker = new MqttBroker();
			
			_broker.Start();
			
			Receive<StopMQTTBroker>(msg => OnStopBroker());
		}

		private void OnStartBroker()
		{
			Become(Started);
		}

		private void OnStopBroker()
		{
			Become(Stopped);
		}
	}
}
