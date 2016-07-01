using System;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Akka.IoT.MQTT.Infrastructure.Dtos;
using Akka.IoT.MQTT.Infrastructure.Topics;
using Akka.IoT.Shared.Dtos;
using Akka.IoT.Shared.Enums;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace Akka.IoT.Sensor
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private MqttClient _client;
		private string _deviceId;
		private readonly string _mqttBrokerAdress;
		private ushort _disconnectMsgId;
		private bool _deviceState;

		public MainWindow()
		{
			InitializeComponent();

			_client = null;
			
			_deviceState = false;
			_mqttBrokerAdress = ConfigurationManager.AppSettings.Get( "mqttBrokerAddress" );

			SetOnOffButtonLabel();
			SetBackgroundColor();

			txtDeviceId.Text = Guid.NewGuid().ToString();
			_deviceId = txtDeviceId.Text;
		}

		private void CmbDeviceCategory_OnSelectionChanged( object sender, SelectionChangedEventArgs e )
		{
			
		}

		private void BtnConnect_OnClick( object sender, RoutedEventArgs e )
		{
			string deviceName = txtDeviceName.Text;
			if ( !string.IsNullOrWhiteSpace( deviceName ) )
			{
				try
				{
					// create client instance 
					if ( _client == null )
					{
						_client = new MqttClient( _mqttBrokerAdress );
						_client.MqttMsgPublishReceived += ClientOnMqttMsgPublishReceived;
					}

					_client.Connect( txtDeviceId.Text );

					btnConnect.IsEnabled = false;
					btnDisconnect.IsEnabled = true;
					btnOnOff.IsEnabled = true;
					txtDeviceId.IsReadOnly = true;
					_deviceId = txtDeviceId.Text;
				}
				catch ( Exception exc )
				{
					MessageBox.Show( "An error occurred connecting to MQTT broker." +
									Environment.NewLine +
									Environment.NewLine +
									"Mqtt broker address: " + _mqttBrokerAdress +
									Environment.NewLine +
									Environment.NewLine +
									"Error: " + exc.Message,
									"Error",
									MessageBoxButton.OK,
									MessageBoxImage.Error );
				}

				int deviceGroup = int.Parse(cmbGroup.Text);
				DeviceTypes deviceType = (DeviceTypes)Enum.Parse(typeof(DeviceTypes), cmbDeviceType.Text);

				MqttDeviceInfoDto dto = new MqttDeviceInfoDto(_deviceId, deviceName, deviceGroup, deviceType);
				byte[] dtoConverted = null;
				BinaryFormatter bf = new BinaryFormatter();
				using ( MemoryStream ms = new MemoryStream() )
				{
					bf.Serialize(ms, dto);
					dtoConverted = ms.ToArray();
				}
				
				// Encoding.UTF8.GetBytes(strValue)
				// publish a message on "/home/temperature" topic with QoS 2 
				_client.Subscribe( new string[] {DeviceTopic.Registered + "/" + _deviceId}, new byte[] {MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE} );
				_client.Subscribe(new string[] { DeviceTopic.Unregistered + "/" + _deviceId }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
				_client.Subscribe(new string[] { DeviceTopic.SetDeviceState + "/" + _deviceId }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
				_client.Publish(DeviceTopic.Register, dtoConverted, MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false); 
			}
			else
			{
				MessageBox.Show( "Device name cannot be empty",
								"Warning",
								MessageBoxButton.OK,
								MessageBoxImage.Warning );
			}
		}

		private void BtnOnOff_OnClick( object sender, RoutedEventArgs e )
		{
			_deviceState = !_deviceState;
			MqttDeviceStateDto deviceStateDto = new MqttDeviceStateDto(_deviceState);
			_client.Publish(DeviceTopic.DeviceStateChanged + "/" + _deviceId, deviceStateDto.Serialize(), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
			SetOnOffButtonLabel();
		}

		private void SetOnOffButtonLabel()
		{
			btnOnOff.Content = _deviceState ? "Switch to OFF" : "Switch to ON";
			SetBackgroundColor();
		}

		private void SetBackgroundColor()
		{
			this.Dispatcher.Invoke(() => this.Background = _deviceState ? new SolidColorBrush(Colors.LightGreen) : new SolidColorBrush(Colors.LightCoral));
		}

		private void BtnDisconnect_OnClick( object sender, RoutedEventArgs e )
		{
			_client.Publish(DeviceTopic.Unregister + "/" + _deviceId, new byte[0], MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
			_client.Unsubscribe( new string[1] {DeviceTopic.SetDeviceState + "/" + _deviceId} );
		}

		private void Window_Closing(object sender, CancelEventArgs e)
		{
			_client.MqttMsgPublished += ClientOnMqttMsgPublished;
			_disconnectMsgId = _client.Publish(DeviceTopic.Disconnect + "/" + _deviceId, new byte[0], MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
		}

		private void ClientOnMqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs mqttMsgPublishEventArgs)
		{
			if (mqttMsgPublishEventArgs.Topic == DeviceTopic.Unregistered + "/" + _deviceId)
			{
				if (_client != null && _client.IsConnected)
					_client.Disconnect();

				btnConnect.Dispatcher.Invoke(() => { btnConnect.IsEnabled = true; });
				btnDisconnect.Dispatcher.Invoke(() => { btnDisconnect.IsEnabled = false; });
				btnOnOff.Dispatcher.Invoke(() => { btnOnOff.IsEnabled = false; });
			}
			else if(mqttMsgPublishEventArgs.Topic == DeviceTopic.SetDeviceState + "/" + _deviceId)
			{
				_deviceState = MqttDeviceStateDto.Deserialize( mqttMsgPublishEventArgs.Message ).CurrentState;
				btnOnOff.Dispatcher.Invoke(() => btnOnOff.Content = _deviceState ? "Switch to OFF" : "Switch to ON");
				SetBackgroundColor();
				_client.Publish(DeviceTopic.SetDeviceStateChanged + "/" + _deviceId, (new MqttDeviceStateDto(_deviceState)).Serialize(), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
			}
		}

		private void ClientOnMqttMsgPublished( object sender, MqttMsgPublishedEventArgs e )
		{
			if(e.MessageId == _disconnectMsgId)
				_client.Disconnect();
		}
	}
}
