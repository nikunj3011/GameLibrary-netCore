using Crypto.API;
using GameLibrary.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nancy.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Ditech.Portal.NET.Controllers
{
    public class CryptoController : Controller
    {
        private readonly ILogger<CryptoController> ilogger;
        private readonly IMailService mailService;

        public CryptoController(ILogger<CryptoController> ilogger, IMailService mailService)
        {
            this.ilogger = ilogger;
            this.mailService = mailService; 
        }

        public IActionResult Index()
        {
            MailRequest mailRequest = new MailRequest { Attachments = null, Body = "Crypto Price", Subject = "buy it", ToEmail = "nikunjrathod3011@gmail.com" };
            var webClient = new WebClient();
            var json = webClient.DownloadString("https://api.coingecko.com/api/v3/coins/markets?vs_currency=cad&order=market_cap_desc&per_page=10&page=1&sparkline=false");
            //JObject o1 = JObject.Parse(@json);
            var json2 = webClient.DownloadString("https://api.coingecko.com/api/v3/coins/markets?vs_currency=cad&ids=monero&order=market_cap_desc&per_page=100&page=1&sparkline=false");
            //JObject o2 = JObject.Parse(@json2);
            //o1.Merge(o2, new JsonMergeSettings
            //{
            //    // union array values together to avoid duplicates
            //    MergeArrayHandling = MergeArrayHandling.Union
            //});
            var operations = JsonConvert.DeserializeObject<List<CryptoViewModel>>(json);
            var operations2 = JsonConvert.DeserializeObject<List<CryptoViewModel>>(json2);
            StringBuilder builder = new StringBuilder();
            operations.Add(operations2.FirstOrDefault());
            var checkPriceLow = operations.AsEnumerable()
                .Where(o => o.id == "ethereum" && o.current_price <= o.low_24h + 100
                || o.id == "bitcoin" && o.current_price <= o.low_24h + 700
                || o.id == "monero" && o.current_price <= o.low_24h + 15).ToList();
            if (checkPriceLow.Count() >= 1)
            {
                //var student = JsonConvert.DeserializeObject<List<CryptoViewModel>>(checkPriceLow);
                //var students = new JavaScriptSerializer().Deserialize<List<CryptoViewModel>>(checkPriceLow);
                //string combined = string.Join("", checkPriceLow.Take(50)).ToString();
                //var _values = checkPriceLow.AsEnumerable().Select(x => x); 
                ilogger.LogInformation("buy");
                mailRequest.Body = json+json2;
                mailRequest.Subject = "Sell";
                mailService.SendEmailAsync(mailRequest);
            }

            var checkPriceHigh = operations
                .Where(o => o.id == "ethereum" && o.current_price >= o.high_24h - 100
                || o.id == "bitcoin" && o.current_price >= o.high_24h - 700
                || o.id == "monero" && o.current_price >= o.high_24h - 15);
            if (checkPriceHigh.Count() >= 1)
            {
                ilogger.LogInformation("sell");
                mailRequest.Body = json + json2;
                mailRequest.Subject = "Sell";
                mailService.SendEmailAsync(mailRequest);
            }
            HttpContext.Response.Headers.Add("refresh", "120; url=" + Url.Action("Index"));
            return View(operations.ToList());
        }
    }
}
