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
				var responseBody = await GetResponseBody (xmlLabel.Text);
				using (var stream = new StringReader (responseBody)) {
					var serializer = new XmlSerializer (typeof(GeocoderObject));
					var geocoderObject = serializer.Deserialize (stream);
				}
			} else if (sender == btnPaseJsonStatic) {
				var responseBody = await GetResponseBody (jsonLabel.Text);
				var geocoderObject = JsonConvert.DeserializeObject<GeocoderObject> (responseBody);
			} else if (sender == btnParseJsonDynamically) {
				var responseBody = await GetResponseBody (jsonLabel.Text);
				dynamic geocderObject = JsonConvert.DeserializeObject<ExpandoObject> (responseBody);
				DisplayAlert ("Success!", geocderObject.results [0].formatted_address, "OK");
			} else {
				return;
			}
		}

		private async Task<string> GetResponseBody (string requestBody)
		{
			var request = WebRequest.Create (requestBody); //@"http://some-server.com/file.xml;
			request.ContentType = "text/xml/json";
			request.Method = HttpMethod.Get;

			using (var response = await request.GetResponseAsync () as HttpWebResponse) {
				if (response.StatusCode == HttpStatusCode.OK) {
					using (StreamReader reader = new StreamReader (response.GetResponseStream ())) {
						return reader.ReadToEnd ();     
					}
				} else {
					return string.Empty;
				}
			}
		}
	}


}

