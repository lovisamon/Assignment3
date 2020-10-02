using MAD = Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using SharedLibraries.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibraries.Services
{
    public static class DeviceService
    {
        public static readonly Random rnd = new Random();
        public static async Task SendMessageAsync(DeviceClient deviceClient)
        {
            while (true)
            {
                string _json = JsonConvert.SerializeObject(new TemperatureModel(rnd.Next(20, 40), rnd.Next(0, 20)));

                Message payload = new Message(Encoding.UTF8.GetBytes(_json));

                await deviceClient.SendEventAsync(payload);

                Console.WriteLine($"Message sent: {_json}");

                await Task.Delay(60 * 1000);
            }
        }

        public static async Task ReceiveMessageAsync(DeviceClient deviceClient)
        {
            while (true)
            {
                var payload = await deviceClient.ReceiveAsync();

                if (payload == null)
                {
                    continue;
                }

                Console.WriteLine($"Message received: {Encoding.UTF8.GetString(payload.GetBytes())}");

                await deviceClient.CompleteAsync(payload);
            }
        }

        public static async Task SendMessageToDeviceAsync(MAD.ServiceClient serviceClient, string targetDeviceID, string message)
        {
            var payload = new MAD.Message(Encoding.UTF8.GetBytes(message));
            await serviceClient.SendAsync(targetDeviceID, payload);
        }
    }
}
