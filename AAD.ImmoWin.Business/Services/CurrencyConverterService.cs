using AAD.ImmoWin.Business.Classes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AAD.ImmoWin.Business.Services
{
    public static class CurrencyConverterService
    {
        public static async Task<JObject> APICallCurrencyList(String baseCurrencyCode = "EUR")
        {
            try
            {
                RestClient client = new RestClient(new RestClientOptions(baseUrl: "https://api.currencyapi.com/v3/latest")
                {
                    MaxTimeout = 10000
                });
                RestRequest request = new RestRequest();
                request.Method = Method.Get;
                request.AddHeader("apikey", "cur_live_7KP9yMNqONaRFocF6qzvlqKRAmJpK5ekYnlwGBLS");
                request.AddParameter("base_currency", baseCurrencyCode);
                RestResponse response = client.Execute(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    JObject jsonResponse = JObject.Parse(response.Content);
                    JObject data = (JObject)jsonResponse["data"];

                    DateTime currentTime = DateTime.UtcNow;

                    JObject result = new JObject
                    {
                        { "DateTime", currentTime },
                        { "APIResponse", data }
                    };

                    UpdateFile(result);

                    return result;
                }
            }
            catch
            {
                return null;
            }
            return null;
        }

        private static void UpdateFile(JObject apiResult)
        {
            string jsonResult = JsonConvert.SerializeObject(apiResult, Formatting.Indented);

            File.WriteAllText("APIResponse.json", jsonResult);
        }

        public static Dictionary<string, decimal?> GetCurrencies()
        {
            string filePath = "APIResponse.json";

            if (!File.Exists(filePath))
            {
                APICallCurrencyList("EUR");
            }
            
            String json = File.ReadAllText(filePath);
            ApiResponse apiResponse = JsonConvert.DeserializeObject<ApiResponse>(json);

            Dictionary<string, decimal?> currencies = new Dictionary<string, decimal?>();
            foreach (KeyValuePair<string, Currency> entry in apiResponse.APIResponse)
            {
                currencies.Add(entry.Key, entry.Value.Value);
            }

            return currencies;
        }

        public static List<String> GetCurrencyList(String currencyCode)
        {
            if (CheckLastApiCall() < DateTime.Now.AddHours(-12))
            {
                APICallCurrencyList("EUR");
            }
            List<String> currencyList = new List<String>();
            foreach(KeyValuePair<string, decimal?> currency in GetCurrencies())
            {
                currencyList.Add(currency.Key);
            }
            return currencyList;
        }

        public static decimal? ConvertFromEuroTo(String currencyCode, decimal? baseValue)
        {
            if(CheckLastApiCall() is null)
            {
                return 0;
            }
            Dictionary<string, decimal?> conversionList = GetCurrencies();
            if (conversionList.ContainsKey(currencyCode))
            {
                decimal? test = baseValue * conversionList[currencyCode].Value;
                return baseValue * conversionList[currencyCode].Value;
            }
            return null;
        }


        public static DateTime? CheckLastApiCall()
        {
            string filePath = "APIResponse.json";

            if (!File.Exists(filePath))
            {
                APICallCurrencyList("EUR");
            }

            String json = File.ReadAllText(filePath);
            ApiResponse apiResponse = JsonConvert.DeserializeObject<ApiResponse>(json);
            return apiResponse.DateTime;
        }

        public static decimal RoundWaarde(decimal waarde)
        {
            return Math.Round(waarde, 2);
        }
    }
}