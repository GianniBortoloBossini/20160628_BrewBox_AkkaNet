#region MailUp Header
// MailUp
// 
// Solution: Akka.IoT
// Project: Akka.IoT.App
// File: WebCamActor.cs
// 
// Last update: Gianni Bortolo Bossini on 21-06-2016
#endregion

using System;
using System.Configuration;
using System.IO;
using System.Net;
using Akka.Actor;
using Akka.IoT.App.Exceptions;
using Akka.IoT.Shared.Messages;
using Akka.IoT.Shared.Messags;
using NReco.ImageGenerator;

namespace Akka.IoT.App.Actors
{
	public class SnapshotActor : ReceiveActor
	{
		private readonly string _uriAddress;
		private readonly string _snapshotBasePath;

		public SnapshotActor(Uri uri)
		{
			_uriAddress = uri.ToString();
			_snapshotBasePath = ConfigurationManager.AppSettings["SnapshotBasePath"];

			Receive<TakeSnapshot>( msg => OnTakeSnapshot( msg ) );
		}

		private void OnTakeSnapshot( TakeSnapshot msg )
		{
			string htmlCode;
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(_uriAddress);
			HttpWebResponse response = (HttpWebResponse)request.GetResponse();
			StreamReader sr = new StreamReader(response.GetResponseStream());
			htmlCode = sr.ReadToEnd();

			string fileName = Path.Combine(_snapshotBasePath, msg.Id + "_" + DateTime.UtcNow.ToString("yyyyMMddHHmmss") + ".jpg");
			
			var documentMsg = new TakeSnapshotCompleted(msg.Id, fileName);
			Sender.Tell(documentMsg);

			if (!Directory.Exists(_snapshotBasePath))
				Directory.CreateDirectory(_snapshotBasePath);

			HtmlToImageConverter htmlToImageConv = new NReco.ImageGenerator.HtmlToImageConverter();
			
			try
			{
				byte[] jpegBytes = htmlToImageConv.GenerateImage(htmlCode, ImageFormat.Jpeg);
				File.WriteAllBytes(fileName, jpegBytes);
			}
			catch ( Exception exc)
			{
				throw new GenerateSnapshotException(fileName);
			}

			Context.Parent.Tell(documentMsg);
		}
	}
}