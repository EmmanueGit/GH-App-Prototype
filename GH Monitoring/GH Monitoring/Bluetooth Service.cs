using Plugin.BLE;
using Plugin.BLE.Abstractions.Contracts;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GH_Monitoring
{
    internal class Bluetooth_Service
    {
        private readonly IAdapter _bluetoothAdapter;
        private IDevice _bluetoothDevice;

        public Bluetooth_Service()
        {
            // Initialize the Bluetooth adapter
            _bluetoothAdapter = CrossBluetoothLE.Current.Adapter;
        }

        public async Task<bool> ConnectToBluetoothAsync(string deviceAddress)
        {
            try
            {
                _bluetoothDevice = await ScanForDeviceAsync(deviceAddress);

                if (_bluetoothDevice != null)
                {
                    await _bluetoothAdapter.ConnectToDeviceAsync(_bluetoothDevice);
                    Console.WriteLine("Connected to Bluetooth device.");
                    return true;
                }

                Console.WriteLine("Device not found.");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error connecting to Bluetooth device: {ex.Message}");
                return false;
            }
        }

        public async Task SendDataAsync(string data)
        {
            try
            {
                if (_bluetoothDevice != null)
                {
                    var services = await _bluetoothDevice.GetServicesAsync();
                    var characteristic = services
                        .SelectMany(service => service.GetCharacteristicsAsync().Result)
                        .FirstOrDefault();

                    if (characteristic != null)
                    {
                        // Convert string data to byte array and write
                        byte[] byteData = Encoding.ASCII.GetBytes(data);
                        await characteristic.WriteAsync(byteData);
                        Console.WriteLine("Data sent.");
                    }
                    else
                    {
                        Console.WriteLine("No suitable characteristic found.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending data: {ex.Message}");
            }
        }

        private async Task<IDevice> ScanForDeviceAsync(string deviceAddress)
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
            await Task.Delay(10000); // Timeout after 10 seconds

            return foundDevice;
        }
    }
}
