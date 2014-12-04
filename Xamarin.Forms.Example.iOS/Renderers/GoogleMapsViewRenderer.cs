using System;
using Xamarin.Forms.Platform.iOS;
using Google.Maps;
using System.Drawing;
using Xamarin.Forms;
using Xamarin.Forms.Example;
using Xamarin.Forms.Example.iOS;

[assembly: ExportRenderer (typeof(GoogleMapsView), typeof(GoogleMapsViewRenderer))]
namespace Xamarin.Forms.Example.iOS
{
	public class GoogleMapsViewRenderer : ViewRenderer<GoogleMapsView, MapView>
	{
		protected override void OnElementChanged (ElementChangedEventArgs<GoogleMapsView> e)
		{
			base.OnElementChanged (e);
			if (e.OldElement != null || this.Element == null)
				return;

			var camera = CameraPosition.FromCamera (latitude: 37.797865, longitude: -122.402526, zoom: 6);
			var mapView = MapView.FromCamera (RectangleF.Empty, camera);
			mapView.MyLocationEnabled = true;

			SetNativeControl (mapView);
		}
	}
}

