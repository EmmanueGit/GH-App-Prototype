using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GH_Monitoring
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // Wrap the MainPage in a NavigationPage
            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
