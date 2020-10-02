using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SharedLibraries.Models;
using SharedLibraries.Services;
using Microsoft.Azure.Devices;

namespace AzureFunctions
{
    public static class SendMessageToDevice
    {

        private static readonly ServiceClient serviceClient =
            ServiceClient.CreateFromConnectionString(Environment.GetEnvironmentVariable("IoTHubConnection"));

        [FunctionName("SendMessageToDevice")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {

            // GET:
            // http://localhost:7071/api/SendMessageToDevice?targetDeviceID=IoTDevice&message=dettaarmeddelandet
            string targetDeviceID = req.Query["targetDeviceID"];
            string message = req.Query["message"];

            // POST:
            // Http Body = { "targetdeviceid": "iotdevice", "message": "detta ar meddelandet"}
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            var data = JsonConvert.DeserializeObject<BodyMessageModel>(requestBody);

            targetDeviceID = targetDeviceID ?? data?.TargetDeviceID;
            message = message ?? data?.Message;

            await DeviceService.SendMessageToDeviceAsync(serviceClient, targetDeviceID, message);

            return new OkResult();
        }
    }
}
