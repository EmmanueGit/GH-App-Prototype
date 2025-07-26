using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;
using Plugin.BLE;
using Plugin.BLE.Abstractions.Contracts;
using System.Linq;
using System.Text;
using Xamarin.Forms.Xaml;

namespace GH_Monitoring
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BluetoothPage : ContentPage
    {
        private BluetoothPageViewModel _viewModel;

        public BluetoothPage()
        {
            InitializeComponent();
            _viewModel = new BluetoothPageViewModel();
            BindingContext = _viewModel;
        }
    }

    public class BluetoothPageViewModel : BindableObject
    {
        private string _connectionStatus;
        private string _deviceResponse;
        private string _selectedDevice;
        private List<string> _devices;
        private IAdapter _bluetoothAdapter;
        private IDevice _bluetoothDevice;

        public BluetoothPageViewModel()
        {
            _bluetoothAdapter = CrossBluetoothLE.Current.Adapter;
            Devices = new List<string> { "Device 1", "Device 2", "Device 3" };
            ConnectionStatus = "Disconnected";
            DeviceResponse = "Waiting for command...";

            // Command to connect to Bluetooth device
            ConnectCommand = new Command(async () => await ConnectToDevice());
        }

        public ICommand ConnectCommand { get; }

        public List<string> Devices
        {
            get => _devices;
            set
            {
                _devices = value;
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

        public string ConnectionStatus
        {
            get => _connectionStatus;
            set
            {
                _connectionStatus = value;
                OnPropertyChanged();
            }
        }

        public string DeviceResponse
        {
            get => _deviceResponse;
            set
            {
                _deviceResponse = value;
                OnPropertyChanged();
            }
        }

        private async System.Threading.Tasks.Task ConnectToDevice()
        {
            if (string.IsNullOrEmpty(SelectedDevice))
            {
                DeviceResponse = "Please select a device!";
                ConnectionStatus = "Disconnected";
                return;
            }

            try
            {
                // Attempt to connect to the selected device
                var device = await ScanForDeviceAsync(SelectedDevice);
                if (device != null)
                {
                    await _bluetoothAdapter.ConnectToDeviceAsync(device);
                    _bluetoothDevice = device;

                    ConnectionStatus = "Connected to " + SelectedDevice;
                    DeviceResponse = "Device connected. Ready for interaction!";
                }
                else
                {
                    DeviceResponse = "Device not found!";
                    ConnectionStatus = "Disconnected";
                }
            }
            catch (Exception ex)
            {
                DeviceResponse = $"Error: {ex.Message}";
                ConnectionStatus = "Disconnected";
            }
        }

        private async System.Threading.Tasks.Task<IDevice> ScanForDeviceAsync(string deviceAddress)
        {
            IDevice foundDevice = null;

            // Start scanning for devices
            await _bluetoothAdapter.StartScanningForDevicesAsync();

            // Attach the event handler for device discovery
            _bluetoothAdapter.DeviceDiscovered += (sender, e) =>
            {
                if (e.Device.Id.ToString() == deviceAddress)
                {
                    foundDevice = e.Device;
                    _bluetoothAdapter.StopScanningForDevicesAsync();  // Stop scanning once device is found
                }
            };

            // Wait for the scan to complete or find the device
            await System.Threading.Tasks.Task.Delay(10000); // Timeout after 10 seconds

            return foundDevice;
        }
    }
}
