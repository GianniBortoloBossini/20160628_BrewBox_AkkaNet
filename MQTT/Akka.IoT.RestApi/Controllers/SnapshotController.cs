#region MailUp Header
// MailUp
// 
// Solution: Akka.IoT
// Project: Akka.IoT.RestApi
// File: WebCamController.cs
// 
// Last update: Gianni Bortolo Bossini on 21-06-2016
#endregion

using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Akka.Actor;
using Akka.IoT.Shared.Messages;
using Akka.IoT.Shared.Messags;

namespace Akka.IoT.RestApi.Controllers
{
	public class SnapshotController : ApiController
	{
		[HttpPost]
		[Route("api/snapshot")]
		public HttpResponseMessage Post([FromBody]dynamic data)
		{

			Uri snapshotUri = new Uri(data.uri.ToString());

			TakeSnapshotCompleted reponse = ActorSystemReferences.Actors.PlantCoordinatorActor.Ask<TakeSnapshotCompleted>(new TakeSnapshot(snapshotUri), TimeSpan.FromSeconds(10)).Result;
			return Request.CreateResponse(HttpStatusCode.OK, reponse);
		}
	}
}