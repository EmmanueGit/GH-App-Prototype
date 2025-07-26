using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace GH_Monitoring
{
    public class MainPageViewModel : BindableObject
    {
        private bool _isMistingOn;
        private bool _isFanOn;
        private string _mistingButtonText;
        private string _fanButtonText;
        private string _temperature;
        private string _humidity;
        private string _soilMoisture;
        private string _waterLevel;
        private double _waterLevelProgress;
        private string _selectedPlant;
        private string _deviceResponse;
        private string _connectionStatus;
        private string _selectedDevice;
        private List<string> _devices;

        // Constructor with initialization of properties
        public MainPageViewModel()
        {
            // Set initial values for the properties
            MistingButtonText = "Misting: Off";
            FanButtonText = "Fan: Off";
            Temperature = "25°C / 77°F";
            Humidity = "50%";
            SoilMoisture = "Dry / 40%";
            WaterLevel = "Full";
            WaterLevelProgress = 1.0;

            // Initialize Bluetooth properties
            Devices = new List<string> { "Device 1", "Device 2", "Device 3" };
            ConnectionStatus = "Disconnected";
            DeviceResponse = "Waiting for command...";

            // Initialize commands
            ToggleMistingCommand = new Command(ToggleMisting);
            ToggleFanCommand = new Command(ToggleFan);
            SendDataCommand = new Command(SendDataToDevice);
            NavigateToBluetoothPageCommand = new Command(NavigateToBluetoothPage);
        }

        // Commands for toggling misting and fan
        public ICommand ToggleMistingCommand { get; }
        public ICommand ToggleFanCommand { get; }
        public ICommand SendDataCommand { get; }
        public ICommand NavigateToBluetoothPageCommand { get; }

        // Properties for the existing device controls
        public string MistingButtonText
        {
            get => _mistingButtonText;
            set
            {
                _mistingButtonText = value;
                OnPropertyChanged();
            }
        }

        public string FanButtonText
        {
            get => _fanButtonText;
            set
            {
                _fanButtonText = value;
                OnPropertyChanged();
            }
        }

        public string Temperature
        {
            get => _temperature;
            set
            {
                _temperature = value;
                OnPropertyChanged();
            }
        }

        public string Humidity
        {
            get => _humidity;
            set
            {
                _humidity = value;
                OnPropertyChanged();
            }
        }

        public string SoilMoisture
        {
            get => _soilMoisture;
            set
            {
                _soilMoisture = value;
                OnPropertyChanged();
            }
        }

        public string WaterLevel
        {
            get => _waterLevel;
            set
            {
                _waterLevel = value;
                OnPropertyChanged();
            }
        }

        public double WaterLevelProgress
        {
            get => _waterLevelProgress;
            set
            {
                _waterLevelProgress = value;
                OnPropertyChanged();
            }
        }

        public string SelectedPlant
        {
            get => _selectedPlant;
            set
            {
                _selectedPlant = value;
                OnPropertyChanged();
            }
        }

        // Bluetooth-related properties
        public string DeviceResponse
        {
            get => _deviceResponse;
            set
            {
                _deviceResponse = value;
                OnPropertyChanged();
            }
        }

        public string ConnectionStatus
        {
            get => _connectionStatus;
            set
            {
                _connectionStatus = value;
                OnPropertyChanged();
            }
        }

        public string SelectedDevice
        {
            get => _selectedDevice;
            set
            {
                _selectedDevice = value;
                OnPropertyChanged();
            }
        }

        public List<string> Devices
        {
            get => _devices;
            set
            {
                _devices = value;
                OnPropertyChanged();
            }
        }

        // Methods for toggling misting and fan states
        private void ToggleMisting()
        {
            _isMistingOn = !_isMistingOn;
            MistingButtonText = _isMistingOn ? "Misting: On" : "Misting: Off";
        }

        private void ToggleFan()
        {
            _isFanOn = !_isFanOn;
            FanButtonText = _isFanOn ? "Fan: On" : "Fan: Off";
        }

        // Bluetooth-related methods
        private async void NavigateToBluetoothPage()
        {
            // Navigate to Bluetooth page
            await Application.Current.MainPage.Navigation.PushAsync(new BluetoothPage());
        }

        private void SendDataToDevice()
        {
            if (string.IsNullOrEmpty(SelectedDevice))
            {
                DeviceResponse = "No device selected!";
                ConnectionStatus = "Disconnected";
                return;
            }

            // Here, you would implement the actual logic for sending data to the device.
            // Example for the sake of the mockup:
            DeviceResponse = "Data sent successfully!";
            ConnectionStatus = "Connected"; // Update connection status after success

            // In a real scenario, you can add logic for connecting via Bluetooth/Wi-Fi and getting responses.
        }
    }
}
