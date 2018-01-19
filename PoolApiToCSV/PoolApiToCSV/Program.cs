using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PoolApiToCSV
{
    public class Api
    {
        public Api()
        {
            Measures = new List<string>();
        }

        public string key { get; set; }
        public string Address { get; set; }
        public List<string> Measures { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var keyValuePairs = ConfigurationManager.AppSettings.AllKeys
                             .Select(key => new KeyValuePair<string, string>(key, ConfigurationManager.AppSettings[key]));
            List<Api> apis = new List<Api>();
            foreach (var appSetting in keyValuePairs.Where(s => s.Key.StartsWith("api", StringComparison.OrdinalIgnoreCase)))
            {
                apis.Add( new Api() { Address = appSetting.Value, key = appSetting.Key.Replace("api_", "") } );
            }

            var client = new HttpClient();
            var filename = ConfigurationManager.AppSettings["csvFileName"];

            if (!File.Exists(filename))
            {
                File.AppendAllText(filename, "DateTime");
                foreach (var appSetting in keyValuePairs.Where(s => s.Key.StartsWith("measure", StringComparison.OrdinalIgnoreCase)))
                {
                    File.AppendAllText(filename, $", {appSetting.Key.Replace("measure_", "")}.{appSetting.Value}");
                }
                File.AppendAllText(filename, "\r\n");
            }

            while (true)
            {
                try
                {
                    File.AppendAllText(filename, DateTime.Now.ToString());

                    foreach (var api in apis)
                    {
                        var response = client.GetAsync(api.Address).Result;
                        string jsonObject = response.Content.ReadAsStringAsync().Result;

                        JObject j = JsonConvert.DeserializeObject(jsonObject) as JObject;
                        foreach (var measure in keyValuePairs.Where(s => s.Key.StartsWith("measure_" + api.key)))
                        {
                            var value = (double) j[measure.Value];
                            File.AppendAllText(filename, $", {value}");
                        }
                    }
                    File.AppendAllText(filename, "\r\n");

                    Thread.Sleep(TimeSpan.FromMinutes(double.Parse(ConfigurationManager.AppSettings["IntervalMinutes"])));
                }
                catch (Exception ex)
                {
                    File.AppendAllText(ConfigurationManager.AppSettings["ErrorLog"], $"\r\n{DateTime.Now}: \r\n{ex}" );
                }
            }
        }
    }
}
