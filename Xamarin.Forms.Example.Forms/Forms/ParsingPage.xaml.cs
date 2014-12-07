﻿using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Net;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;
using System.Dynamic;

namespace Xamarin.Forms.Example
{
	public partial class ParsingPage : ContentPage
	{
		public ParsingPage ()
		{
			InitializeComponent ();
		}

		async void ParseClicked (object sender, EventArgs args)
		{
			var mapPage = new MapPage ();
			await Navigation.PushAsync (mapPage);

			if (sender == btnParseXml) {
				GetResponseBodyAsync (xmlLabel.Text)
					.ContinueWith (async t => {
					var responseBody = await t;
					using (var stream = new StringReader (responseBody)) {
						var serializer = new XmlSerializer (typeof(GeocoderObject));
						var geocoder = (GeocoderObject)serializer.Deserialize (stream);
						mapPage.AddPin (geocoder.Results [0].Geometry.Location, "geocoder", geocoder.Results [0].FormattedAddress);
					}
				});
			} else if (sender == btnPaseJsonStatic) {
				GetResponseBodyAsync (jsonLabel.Text)
					.ContinueWith (async t => {
					var responseBody = await t;
					var geocoder = JsonConvert.DeserializeObject<GeocoderObject> (responseBody);
					mapPage.AddPin (geocoder.Results [0].Geometry.Location, "geocoder", geocoder.Results [0].FormattedAddress);
				});

			} else if (sender == btnParseJsonDynamically) {
				GetResponseBodyAsync (jsonLabel.Text)
					.ContinueWith (async t => {
					var responseBody = await t;
					dynamic geocoderObject = JsonConvert.DeserializeObject (responseBody);
					var location = new Location { Lat = double.Parse (geocoderObject ["results"] [0] ["geometry"] ["location"] ["lat"].ToString ()),
						Long = double.Parse (geocoderObject ["results"] [0] ["geometry"] ["location"] ["lng"].ToString ())
					};
					var address = geocoderObject ["results"] [0] ["formatted_address"].ToString ();
					mapPage.AddPin (location, "geocoder", address);
				});
			} else {
				return;
			}
		}

		private async Task<string> GetResponseBodyAsync (string requestBody)
		{
			var request = WebRequest.Create (requestBody); //@"http://some-server.com/file.xml;
			request.ContentType = "text/xml/json";
			request.Method = HttpMethod.Get;

			using (var response = await request.GetResponseAsync () as HttpWebResponse) {
				if (response.StatusCode == HttpStatusCode.OK) {
					using (StreamReader reader = new StreamReader (response.GetResponseStream ())) {
						var body = reader.ReadToEnd ();     
						return body.ToString ();
					}
				} else {
					return string.Empty;
				}
			}
		}
	}
}

