using System;
using Xamarin.Forms;

namespace Xamarin.Forms.Example
{
	public class App 
	{
		public static Page GetMainPage ()
		{	
			var page = new LoginPage ();
			return new NavigationPage (page);
		}
	}
}

