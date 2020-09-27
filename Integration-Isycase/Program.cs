using System;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Timers;
using DeviceClass;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Serialization;
using IntegrationIsycase;

namespace IntegrationIsycase
{
    
    class Program
    {
        public static void Main()
        {
            var baseUrl = Environment.GetEnvironmentVariable("BASE_URL");
            var alertUrl = Environment.GetEnvironmentVariable("ALERT_URL");
            var apiKey = Environment.GetEnvironmentVariable("API_KEY");

            if (alertUrl == "" || apiKey == "" || baseUrl == "")
            {
                Console.WriteLine("Environment variables BASE_URL, ALERT_URL and API_KEY must be set.");
                System.Environment.Exit(1);
            }
            
            while (true)
            {
                Console.WriteLine("Polling for device status ...");
                GetDeviceStatus(baseUrl, alertUrl, apiKey).Wait();
                Task.Delay(5000).Wait();
            }
        }

        private static Dictionary<string, string> deviceStatus = new Dictionary<string, string>();
        private static HttpClient client = new HttpClient();
        private static async Task GetDeviceStatus(string baseUrl, string alertUrl, string apiKey)
        {
            var url = baseUrl + "/ngsi-ld/v1/entities?type=Device";
            string data = await client.GetStringAsync(url);

            List<Device> devices = JsonConvert.DeserializeObject<List<Device>>(data);

            foreach (var device in devices)
            {
                if (device.Value.Value == "off")
                {
                    if (deviceStatus.ContainsKey(device.Id) && deviceStatus[device.Id] == "on")
                    {
                        Console.WriteLine("Sending alert for device " + device.Id);
                        await PostAlert(device, alertUrl, apiKey);
                    }
                }
                deviceStatus[device.Id] = device.Value.Value;
            }
        }

        private static async Task PostAlert(Device device, string alertUrl, string apiKey)
        {
            // TODO: Read the location from the device
            var alert = new AlertMissingLifebuoy(device.Id, 17.320790, 62.389976);

            var settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                NullValueHandling = NullValueHandling.Ignore
            };

            var json = JsonConvert.SerializeObject(alert, settings);
            var data = new StringContent(json, Encoding.UTF8, "application/x-www-form-urlencoded");

            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("apiKey", apiKey),
                new KeyValuePair<string, string>("alertJsonString", json)
            });
            
            var response = await client.PostAsync(alertUrl, formContent);
            var stringContent = await response.Content.ReadAsStringAsync();

            Console.WriteLine(response.StatusCode + ": " + stringContent);
        }
    }
}