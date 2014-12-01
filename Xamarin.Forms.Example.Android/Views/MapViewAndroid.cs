using System;
using Xamarin.Forms.Maps;
using Xamarin.Forms;
using Xamarin.Forms.Example.Android;

[assembly: Dependency(typeof(MapViewAndroid))]
namespace Xamarin.Forms.Example.Android
{
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
				var span = new MapSpan (position, 0.01, 0.01);
				_map.MoveToRegion (span);
				_map.Pins.Add (new Pin { 
					Type = PinType.Place, 
					Position = position, 
					Label = Label, 
					Address = Address });
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

