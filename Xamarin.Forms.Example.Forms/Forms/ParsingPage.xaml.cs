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
				var responseBody = await GetResponseBodyAsync (xmlLabel.Text);
				using (var stream = new StringReader (responseBody)) {
					var serializer = new XmlSerializer (typeof(GeocoderObject));
					var geocoderObject = serializer.Deserialize (stream);

					await Navigation.PushAsync (new MapPage ());
				}
			} else if (sender == btnPaseJsonStatic) {
				GetResponseBodyAsync (jsonLabel.Text)
					.ContinueWith(async t => {
						var responseBody = await t;
						return JsonConvert.DeserializeObject<GeocoderObject> (responseBody);
					}).ContinueWith(async t => {
						var geocoder = await t;
						await Navigation.PushAsync (new MapPage ());
					}, TaskScheduler.FromCurrentSynchronizationContext());

			} else if (sender == btnParseJsonDynamically) {
				var responseBody = await GetResponseBodyAsync (jsonLabel.Text);
				dynamic geocoderObject = JsonConvert.DeserializeObject(responseBody);
				try {
				var address = geocoderObject["results"][0]["formatted_address"];
				DisplayAlert ("Success!", address.ToString(), "OK");
				} catch (Exception ex) {
					DisplayAlert ("Whoops", ex.Message, "OK");
				}
			} else {
				return;
			}
		}

		async void GoToMaps (object geocoderObject)
		{
			try {
				var mapPage = DependencyService.Get<IMapView> ();
				var geocoder = (geocoderObject as GeocoderObject).Results [0];
				mapPage.AddPin (geocoder.Geometry.Location, "geocoder", geocoder.FormattedAddress);
				await Navigation.PushAsync ((Page)mapPage);
			}
			catch (Exception ex) {
				DisplayAlert ("Whoops", ex.Message, "OK");
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

