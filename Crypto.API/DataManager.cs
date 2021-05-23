using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Crypto.API
{ 
    public class DataManager
    {
        public static List<CryptoViewModel> GetData()
        {
            var webClient = new WebClient();
            var json = webClient.DownloadString("https://api.coingecko.com/api/v3/coins/markets?vs_currency=cad&order=market_cap_desc&per_page=10&page=1&sparkline=false");
            var operations = JsonConvert.DeserializeObject<List<CryptoViewModel>>(json);
            return operations.ToList();
        }
    }
}
