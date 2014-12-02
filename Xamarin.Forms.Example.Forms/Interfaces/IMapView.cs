using System;

namespace Xamarin.Forms.Example
{
	public interface IMapView
	{
		void AddPin(Location location, string label, string address);
	}
}

