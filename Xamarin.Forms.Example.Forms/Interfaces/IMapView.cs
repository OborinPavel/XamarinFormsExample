using System;

namespace Xamarin.Forms.Example
{
	public interface IMapView
	{
		Location Location { get; set; }
		String Label { get; set; }
		string Address { get; set; }
		View MapView { get; }
	}
}

