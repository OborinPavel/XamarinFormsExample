using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Example.Android;
using Xamarin.Forms.Maps;

[assembly: Dependency(typeof(GoogleMapAndroid))]
namespace Xamarin.Forms.Example.Android
{	
	public partial class GoogleMapAndroid : ContentPage, IMapView
	{
		#region IMapView implementation

		public void AddPin(Location location, string label, string address) {
			var position = new Position (location.Lat, location.Long);
			var span = new MapSpan (position, 0.01, 0.01);
			map.MoveToRegion (span);
			map.Pins.Add (new Pin { 
				Type = PinType.Place, 
				Position = position, 
				Label = label, 
				Address = address });
		}
		#endregion

		public GoogleMapAndroid ()
		{
			InitializeComponent ();
		}
	}
}

