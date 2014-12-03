using System;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Example;
using Xamarin.Forms;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Xamarin.Forms.Example.Android;
using Android.App;
using Android.OS;

[assembly: ExportRenderer (typeof(GoogleMapsView), typeof(GoogleMapsViewRenderer))]
namespace Xamarin.Forms.Example.Android
{
	public class GoogleMapsViewRenderer : ViewRenderer<GoogleMapsView, MapView>
	{
		protected override void OnElementChanged (ElementChangedEventArgs<GoogleMapsView> e)
		{
			base.OnElementChanged (e);
			if (e.OldElement != null || this.Element == null)
				return;

			GoogleMapOptions options = new GoogleMapOptions ();
			options.InvokeCamera (new CameraPosition (new LatLng (0, 0), 1, 0, 0));          
			var mapView = new MapView (Forms.Context, options);
			mapView.OnCreate (new Bundle());
			mapView.OnResume ();

			SetNativeControl (mapView);
		}

	}
}

