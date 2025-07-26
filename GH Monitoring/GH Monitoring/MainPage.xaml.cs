using System;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace GH_Monitoring
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainPageViewModel();  // Assign ViewModel to BindingContext
        }

        // Bluetooth permission request
        private async void RequestBluetoothPermissions()
        {
            var status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
            if (status != PermissionStatus.Granted)
            {
                await DisplayAlert("Permission Required",
                                   "Location permission is required for Bluetooth to work properly.",
                                   "OK");
            }
        }

    }
}
