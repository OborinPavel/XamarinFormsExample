using System;
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
			if (sender == btnParseXml) {
				GetResponseBodyAsync (xmlLabel.Text)
					.ContinueWith (async t => {
						var responseBody = await t;
						using (var stream = new StringReader (responseBody)) {
							var serializer = new XmlSerializer (typeof(GeocoderObject));
							return serializer.Deserialize (stream);
						}
					}).ContinueWith (async t => {
							var geocoder = await t.Result;
							await Navigation.PushAsync (new MapPage ());
					}, TaskScheduler.FromCurrentSynchronizationContext ());
			} else if (sender == btnPaseJsonStatic) {
				GetResponseBodyAsync (jsonLabel.Text)
					.ContinueWith(async t => {
						var responseBody = await t;
						return JsonConvert.DeserializeObject<GeocoderObject> (responseBody);
					}).ContinueWith(async t => {
						var geocoder = await t.Result;
						var mapPage = new MapPage();
						await Navigation.PushAsync (mapPage);
						mapPage.AddPin(geocoder);
					}, TaskScheduler.FromCurrentSynchronizationContext());

			} else if (sender == btnParseJsonDynamically) {
				GetResponseBodyAsync (jsonLabel.Text)
					.ContinueWith(async t => {
						var responseBody = await t;
						dynamic geocoderObject = JsonConvert.DeserializeObject(responseBody);
						var address = geocoderObject["results"][0]["formatted_address"];
						DisplayAlert ("Success!", address.ToString(), "OK");
					}, TaskScheduler.FromCurrentSynchronizationContext());
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
						return body.ToString();
					}
				} else {
					return string.Empty;
				}
			}
		}
	}
}

