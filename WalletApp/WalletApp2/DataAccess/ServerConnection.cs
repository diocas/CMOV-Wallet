using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WalletApp.DataModel;

namespace WalletApp.DataAccess
{


    class ServerConnection
    {
        private static string server = "http://windows-diogo:56014/";
        private static string service = "CurrencyServices.svc/";
        public static ObservableCollection<Currency> Currencies { get; set; }


        public static async Task<ObservableCollection<Currency>> FetchCurrencies()
        {
            using (HttpClient client = new HttpClient())
            {
                var result = await client.GetAsync(server + service + "CurrencyList?cache=" + Guid.NewGuid().ToString());

                result.EnsureSuccessStatusCode();

                return new ObservableCollection<Currency>(JsonConvert.DeserializeObject<IEnumerable<Currency>>(await result.Content.ReadAsStringAsync()));

            }
        }
    }
}
