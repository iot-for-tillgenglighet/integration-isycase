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
            while (true)
            {
                GetDeviceStatus().Wait();
                Task.Delay(5000).Wait();
            }
        }

        private static Dictionary<string, string> deviceStatus = new Dictionary<string, string>();
        private static HttpClient client = new HttpClient();
        private static async Task GetDeviceStatus()
        {
            var url = "https://iotsundsvall.se/ngsi-ld/v1/entities?type=Device";
            string data = await client.GetStringAsync(url);

            List<Device> devices = JsonConvert.DeserializeObject<List<Device>>(data);

            foreach (var device in devices)
            {
                if (device.Value.Value == "off")
                {
                    if (!deviceStatus.ContainsKey(device.Id) || deviceStatus[device.Id] == "on")
                    {
                        Console.WriteLine("Skicka felrapport för " + device.Id);
                        //Post to kommunen/isycase
                        await PostAlert(device, url);
                    }
                }
                deviceStatus[device.Id] = device.Value.Value;
            }
        }

        private static async Task PostAlert(Device device, string url)
        {
            var alert = new AlertMissingLifebuoy(device.Id, 17.320790, 62.389976);

            var settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                NullValueHandling = NullValueHandling.Ignore
            };

            var json = JsonConvert.SerializeObject(alert, settings);
            var data = new StringContent(json, Encoding.UTF8, "application/x-www-form-urlencoded");
            var alertUrl = Environment.GetEnvironmentVariable("ALERT_URL");
            var apiKey = Environment.GetEnvironmentVariable("API_KEY");

            Console.WriteLine(json);

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