using System;
using System.Reflection.Metadata;
using Microsoft.Azure.Devices.Client;
using SharedLibraries.Services;

namespace IotDevice
{
    class Program
    {
        private static readonly string _conn = "HostName=ec-win20-ass3-iothub.azure-devices.net;DeviceId=IoTDevice;SharedAccessKey=2vh23SxhEp2EC+oXs5gQ8NEZRjybcPHYXc0jKwO53y0=";
        private static readonly DeviceClient deviceClient = DeviceClient.CreateFromConnectionString(_conn, TransportType.Mqtt);
        static void Main(string[] args)
        {
            DeviceService.SendMessageAsync(deviceClient).GetAwaiter();
            DeviceService.ReceiveMessageAsync(deviceClient).GetAwaiter();
            Console.ReadKey();
        }
    }
}
