using System;
using Xamarin.Forms.Maps;

namespace Xamarin.Forms.Example.Android.Android
{
	[assembly: Dependency(typeof(MapViewAndroid))]
	public class MapViewAndroid : IMapView
	{
		Location _location;
		readonly Map _map;

		#region IMapView implementation

		public Location Location { 
			get {
				return _location;
			}
			set {
				_location = value;
				var position = new Position (value.Lat, value.Long);
				_map.MoveToRegion (new MapSpan (_map.VisibleRegion.Center, position.Latitude, position.Longitude));
				_map.Pins.Add (new Pin { Type = PinType.Place, Position = position, Label = Label, Address = Address });
			}
		}

		public View MapView { get { return _map; } }

		public string Label { get; set; }
		public string Address { get; set; }
		#endregion

		public MapViewAndroid ()
		{
			_map = new Map { IsShowingUser = true };
		}
	}
}

