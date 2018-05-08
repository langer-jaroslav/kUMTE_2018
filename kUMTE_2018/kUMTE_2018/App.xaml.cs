using System;
using System.IO;
using System.Linq;
using kUMTE_2018.Models;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace kUMTE_2018
{
	public partial class App : Application
	{
	    public static string AppDataDir = string.Empty;
	    public static string AppDataDbString = string.Empty;
        public App ()
		{
			InitializeComponent();

			MainPage = new MainPage();
		}
	    public App(string appDataDir)
	    {
	        InitializeComponent();
	        AppDataDir = appDataDir;
	        AppDataDbString = Path.Combine(App.AppDataDir, AppSetting.DbFileName);
	        InitializeDb();
            MainPage = new NavigationPage(new MainPage());
	    }

	    private void InitializeDb()
	    {
	        using (var conn = new SQLiteConnection(AppDataDbString))
	        {
	            conn.CreateTable<AppSetting>();
	            var data = conn.Table<AppSetting>();
	            if (!data.Any())
	            {
	                var d = new AppSetting()
	                {
	                    AppName = "kUMTE_2018",
	                    Author = "Jaroslav Langer"
	                };
	                conn.Insert(d);
	            }
	        }

        }

        protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
