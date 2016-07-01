#region MailUp Header
// MailUp
// 
// Solution: Akka.IoT
// Project: Akka.IoT.Shared
// File: GetAllDevicesStatusReceived.cs
// 
// Last update: Gianni Bortolo Bossini on 16-06-2016
#endregion

using System.Collections.Generic;
using Akka.IoT.Shared.Dtos;

namespace Akka.IoT.Shared.Messages
{
	public class GetAllDevicesStatusReceived
	{
		public GetAllDevicesStatusReceived( List<DeviceStateDto> list )
		{
			List = list;
		}

		public List<DeviceStateDto> List { get; private set; }
	}
}