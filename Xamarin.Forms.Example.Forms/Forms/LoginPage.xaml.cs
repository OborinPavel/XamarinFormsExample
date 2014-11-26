using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Xamarin.Forms.Example
{	
	public partial class LoginPage : ContentPage
	{	
		public LoginPage ()
		{
			InitializeComponent ();
		}

		async void LoginClicked(object sender, EventArgs args)
		{
			await Navigation.PushAsync (new ParsingPage ());
		}
	}
}

