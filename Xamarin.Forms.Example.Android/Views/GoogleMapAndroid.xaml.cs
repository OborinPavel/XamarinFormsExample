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
//		Location _location;
//		string _label;
//		string _address;

		#region IMapView implementation

		public void AddPin(Location location, string label, string address) {
			map = new Map { IsShowingUser = true };
			var position = new Position (location.Lat, location.Long);
			map.Pins.Add (new Pin { 
				Type = PinType.Place, 
				Position = position, 
				Label = label, 
				Address = address });
			var span = new MapSpan (position, 0.01, 0.01);
			map.MoveToRegion (span);
		}
		#endregion

		public GoogleMapAndroid ()
		{
			InitializeComponent ();
		}

		protected override void OnDisappearing ()
		{
			base.OnDisappearing ();
			map.Pins.Clear ();
		}
	}
}

