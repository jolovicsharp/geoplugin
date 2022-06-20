using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GeoPluginApp
{
    internal class Program
    {
        public class GeoAppApi
        {
            public string geoplugin_request { get; set; }
            public int geoplugin_status { get; set; }
            public string geoplugin_delay { get; set; }
            public string geoplugin_credit { get; set; }
            public string geoplugin_city { get; set; }
            public string geoplugin_region { get; set; }
            public string geoplugin_regionCode { get; set; }
            public string geoplugin_regionName { get; set; }
            public string geoplugin_areaCode { get; set; }
            public string geoplugin_dmaCode { get; set; }
            public string geoplugin_countryCode { get; set; }
            public string geoplugin_countryName { get; set; }
            public int geoplugin_inEU { get; set; }
            public bool geoplugin_euVATrate { get; set; }
            public string geoplugin_continentCode { get; set; }
            public string geoplugin_continentName { get; set; }
            public string geoplugin_latitude { get; set; }
            public string geoplugin_longitude { get; set; }
            public string geoplugin_locationAccuracyRadius { get; set; }
            public string geoplugin_timezone { get; set; }
            public string geoplugin_currencyCode { get; set; }
            public string geoplugin_currencySymbol { get; set; }
            public string geoplugin_currencySymbol_UTF8 { get; set; }
            public double geoplugin_currencyConverter { get; set; }
        }
        public static bool ValidateIPv4(string ipString)
        {
            if (String.IsNullOrWhiteSpace(ipString))
            {
                return false;
            }

            string[] splitValues = ipString.Split('.');
            if (splitValues.Length != 4)
            {
                return false;
            }

            byte tempForParsing;

            return splitValues.All(r => byte.TryParse(r, out tempForParsing));
        }
        static async Task Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Console Application of GeoPluginAPI\n");
        start:
            Console.Write("Type IP Address : ");
            string IPAddress = Console.ReadLine();
            if (ValidateIPv4(IPAddress) == false)
            {
                Console.WriteLine("You entered not valid IP address." +"\n");
                goto start;
            }
            else
            {
                string apiUrl = "http://www.geoplugin.net/json.gp?ip=" + IPAddress;
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(apiUrl);
                string jsonContent = await response.Content.ReadAsStringAsync();
                GeoAppApi deserialize = JsonConvert.DeserializeObject<GeoAppApi>(jsonContent);
                Console.WriteLine("RESULTS" + "\n");
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine("\nYour IP request is : " + deserialize.geoplugin_request);
                Console.WriteLine("IP - Country Name : " + deserialize.geoplugin_countryName);
                Console.WriteLine("IP - Country Code : " + deserialize.geoplugin_countryCode);
                Console.WriteLine("IP - City : " + deserialize.geoplugin_city);
                Console.WriteLine("IP - Region : " + deserialize.geoplugin_region);
                Console.WriteLine("IP - Region Code : " + deserialize.geoplugin_regionCode);
                Console.WriteLine("IP - Region Name : " + deserialize.geoplugin_regionName);
                Console.WriteLine("IP - Area Code : " + deserialize.geoplugin_areaCode);
                Console.WriteLine("IP - DMA Code : " + deserialize.geoplugin_dmaCode);
                Console.WriteLine("IP - In EU ? : " + deserialize.geoplugin_inEU);
                Console.WriteLine("IP - EU VAT RATE : " + deserialize.geoplugin_euVATrate.ToString());
                Console.WriteLine("IP - Continent Code : " + deserialize.geoplugin_continentCode);
                Console.WriteLine("IP - Continent Name : " + deserialize.geoplugin_continentName);
                Console.WriteLine("IP - Latitude : " + deserialize.geoplugin_latitude);
                Console.WriteLine("IP - Longitude : " + deserialize.geoplugin_longitude);
                Console.WriteLine("IP - Location Accuracy Radius : " + deserialize.geoplugin_locationAccuracyRadius);
                Console.WriteLine("IP - Time Zone : " + deserialize.geoplugin_timezone);
                Console.WriteLine("IP - Currency Code : " + deserialize.geoplugin_currencyCode);
                Console.WriteLine("IP - Currency Simbol : " + deserialize.geoplugin_currencySymbol);
                Console.WriteLine("IP - Currencry Simbol UTF-8 : " + deserialize.geoplugin_currencySymbol_UTF8);
                Console.WriteLine("IP - Currency Converter : " + deserialize.geoplugin_currencyConverter);
                Console.WriteLine("IP - GeoPlugin status : " + deserialize.geoplugin_status);
                Console.WriteLine("IP - GeoPlugin delay : " + deserialize.geoplugin_delay +"\n");
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.Write("Check another (Y/N) : ");
                string answer = Console.ReadLine();
                switch(answer)
                {
                    case "Y":
                        goto start;
                    case "y":
                        goto start;
                    case "n":
                        Environment.Exit(0);
                        break;
                    case "N":
                        //Console.Write("Good bye!");
                        Environment.Exit(0);
                        break;
                    default:
                        Environment.Exit(0);
                        break;
                }
            }
        }
    }
}
