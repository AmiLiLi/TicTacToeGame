using System;
using TicTacToe.View;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace TicTacToe
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //simple Page
            //MainPage = new MainPage();

            //add navigation page for current page
            MainPage = new NavigationPage(new MainPage())
            {
                BarBackgroundColor = Color.Green, 
                BarTextColor = Color.White
            };

            this.initApp();

        }

        /// <summary>
        /// Inits the app.
        /// </summary>
        private void initApp()
        {
            if (!Application.Current.Properties.ContainsKey(Global.PROPERTY_PLAY_COUNT))
            {
                Application.Current.Properties.Add(Global.PROPERTY_PLAY_COUNT, 0);
            }
            if (!Application.Current.Properties.ContainsKey(Global.PROPERTY_WIN_COUNT))
            {
                Application.Current.Properties.Add(Global.PROPERTY_WIN_COUNT, 0);
            }
            if (!Application.Current.Properties.ContainsKey(Global.PROPERTY_LOST_COUNT))
            {
                Application.Current.Properties.Add(Global.PROPERTY_LOST_COUNT, 0);
            }

        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
