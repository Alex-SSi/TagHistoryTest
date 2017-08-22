using DataProvider.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TagHistory
{
    class Program
    {
        static void Main(string[] args)
        {
            HttpClient client;
            HttpClientHandler handler = new HttpClientHandler()
            {
                Credentials = new System.Net.NetworkCredential("ssi", "ssississi")
            };
            client = new HttpClient(handler);
            client.BaseAddress = new Uri("http://192.168.1.98:8765/SDIO/");

            DateTime end = DateTime.Now;
            DateTime start = DateTime.Now.AddDays(-21);
            TagHistoryTestInfoModel model = new TagHistoryTestInfoModel();
            model.channel = 5;
            model.endTime = end.ToString().Replace("/","-");
            model.minutesInterval = 1;
            model.slot = 7;
            model.startTime = start.ToString().Replace("/", "-");
            model.defaultValue = 0;

            string json = "json";
            string content = JsonConvert.SerializeObject(model);
            string query = "GetTagHistory/" + 5 + "/Slot/" + 1 + "/StartTime/" + model.startTime + "/EndTime/" + model.endTime;
            HttpResponseMessage response = client.GetAsync(query).Result;
            json = response.Content.ReadAsStringAsync().Result;
            short[] history = JsonConvert.DeserializeObject<short[]>(json);

            foreach(short s in history)
            {
                Console.WriteLine("Short: " + s);
            }

            Console.ReadLine();
        }
    }
    public class TagHistoryTestInfoModel
    {
        public short channel;
        public short slot;
        public string startTime;
        public string endTime;
        public short minutesInterval;
        public short defaultValue;

        //public TagHistoryTestInfoModel();
    }
}
