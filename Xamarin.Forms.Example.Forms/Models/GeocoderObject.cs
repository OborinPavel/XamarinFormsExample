using System.Collections.Generic;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace Xamarin.Forms.Example
{
	[JsonObject]
	[XmlRootAttribute("GeocodeResponse", IsNullable = false)]
	public class GeocoderObject
	{
		[JsonProperty("results")]
		[XmlElement("result")]
		public List<Result> Results { get; set; }

		[JsonProperty("status")]
		[XmlElement("status")]
		public string Status { get; set; }
	}

	public class Result
	{
		[JsonProperty("address_component")]
		[XmlElement("address_component")]
		public List<AddressComponent> AddressComponents { get; set; }

		[JsonProperty("formatted_address")]
		[XmlElement("formatted_address")]
		public string FormattedAddress { get; set; }

		[JsonProperty("geometry")]
		[XmlElement("geometry")]
		public Geometry Geometry { get; set; }

		[JsonProperty("type")]
		[XmlElement("type")]
		public List<string>Ttypes { get; set; }
	}

	public class AddressComponent
	{
		[JsonProperty("long_name")]
		[XmlElement("long_name")]
		public string LongName { get; set; }

		[JsonProperty("short_name")]
		[XmlElement("short_name")]
		public string ShortName { get; set; }

		[JsonProperty("type")]
		[XmlElement("type")]
		public List<string> Types { get; set; }
	}

	public class Geometry
	{
		[JsonProperty("bounds")]
		[XmlElement("bounds")]
		public Bounds Bounds { get; set; }

		[JsonProperty("location")]
		[XmlElement("location")]
		public Location Location { get; set; }

		[JsonProperty("location_type")]
		[XmlElement("location_type")]
		public string LocationType { get; set; }

		[JsonProperty("viewport")]
		[XmlElement("viewport")]
		public Bounds Viewport { get; set; }
	}

	public class Location
	{
		[JsonProperty("lat")]
		[XmlElement("lat")]
		public double Lat { get; set; }

		[JsonProperty("lng")]
		[XmlElement("lng")]
		public double Long { get; set; }

		public override string ToString ()
		{
			return string.Format ("[Location: Lat={0}, Long={1}]", Lat, Long);
		}
	}

	public class Bounds
	{
		[JsonProperty("northeast")]
		[XmlElement("northeast")]
		public Location Northeast { get; set; }

		[JsonProperty("southeast")]
		[XmlElement("southeast")]
		public Location Southwest { get; set; }
	}
}

