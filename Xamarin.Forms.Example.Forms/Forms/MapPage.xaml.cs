using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Xamarin.Forms.Example
{	
	public partial class MapPage : ContentPage
	{	
		public MapPage (Result geocoder) 
			: this() {
			var mapView = DependencyService.Get<IMapView> ();

			slStack.Children.Add (mapView.MapView);

			mapView.Address = geocoder.FormattedAddress;
			mapView.Label = "geocoder";
			mapView.Location = geocoder.Geometry.Location;
		}

		public MapPage ()
		{
			InitializeComponent ();
		}
	}
}

