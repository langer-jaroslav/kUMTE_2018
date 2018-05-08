using System;
using Xamarin.Forms;

namespace kUMTE_2018
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
		}
	    private void Setting_OnClicked(object sender, EventArgs e)
	    {
	        Navigation.PushAsync(new SettingPage());
	    }

	    private void Browse_OnClicked(object sender, EventArgs e)
	    {

	    }
    }
}
