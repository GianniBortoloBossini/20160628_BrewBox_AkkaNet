#region MailUp Header
// MailUp
// 
// Solution: Akka.IoT
// Project: Akka.IoT.RestApi
// File: DeviceController.cs
// 
// Last update: Gianni Bortolo Bossini on 15-06-2016
#endregion

using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Akka.Actor;
using Akka.IoT.Shared.Messages;

namespace Akka.IoT.RestApi.Controllers
{
	public class DeviceController : ApiController
	{
		public DeviceController()
		{
			
		}

		// GET api/device
		[HttpGet]
		[Route("api/device")]
		public HttpResponseMessage Get()
		{
			GetAllDevicesStatusReceived reponse = ActorSystemReferences.Actors.PlantCoordinatorActor.Ask<GetAllDevicesStatusReceived>( new GetAllDevicesStatus(),
																								TimeSpan.FromSeconds( 10 ) ).Result;
			return Request.CreateResponse(HttpStatusCode.OK, reponse.List);
		}

		// GET api/device/{deviceId} 
		[HttpGet]
		[Route("api/device/{id}")]
		public HttpResponseMessage Get([FromUri]string id)
		{
			GetDeviceStatusReceived reponse = ActorSystemReferences.Actors.PlantCoordinatorActor.Ask<GetDeviceStatusReceived>(new GetDeviceStatus(id),
																								TimeSpan.FromSeconds(10)).Result;
			return Request.CreateResponse(HttpStatusCode.OK, reponse);
		}

		// PUT api/values/5 
		[Route("api/device/{id}")]
		public HttpResponseMessage Put([FromUri]string id, [FromUri]bool status)
		{
			SetDeviceStatusReceived response = ActorSystemReferences.Actors.PlantCoordinatorActor.Ask<SetDeviceStatusReceived>(new SetDeviceStatus(id, status), TimeSpan.FromSeconds(10)).Result;

			if ( response.UnexistingDevice )
				return Request.CreateErrorResponse( HttpStatusCode.BadRequest, "Unexisting device");
			else
				return Request.CreateResponse( HttpStatusCode.OK, response );
		}
	}
}